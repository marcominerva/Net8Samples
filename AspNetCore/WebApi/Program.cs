using TinyHelpers.AspNetCore.Extensions;
using TinyHelpers.Http;
using WebApi.BusinessLayer.Services;
using WebApi.BusinessLayer.Services.Interfaces;
using WebApi.BusinessLayer.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true);

// Add services to the container.
var appSettings = builder.Services.ConfigureAndGet<AppSettings>(builder.Configuration, nameof(AppSettings))!;

builder.Services.AddMemoryCache();

builder.Services.AddTransient(_ =>
{
    var handler = new QueryStringInjectorHttpClientHandler(_ =>
    {
        var headers = new Dictionary<string, string>
        {
            ["units"] = "metric",
            ["APPID"] = appSettings.OpenWeatherMapApiKey
        };

        return Task.FromResult(headers);
    });

    return handler;
});

builder.Services.AddHttpClient<IWeatherService, WeatherService>(client =>
{
    client.BaseAddress = new Uri(appSettings.OpenWeatherMapUrl);
})
.AddHttpMessageHandler<QueryStringInjectorHttpClientHandler>();

builder.Services.AddScoped<IFileImporter, CvsImporter>();
builder.Services.AddScoped<IFileImporter, ExcelImporter>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/weatherforecast", async (IWeatherService weatherService, string city, int days = 5) =>
{
    var forecast = await weatherService.GetForecastAsync(city, days);
    return forecast;
})
.WithOpenApi();

var importerApiGroup = app.MapGroup("/api/import").DisableAntiforgery();

importerApiGroup.MapPost("csv", async (IFormFile file, IFileImporter importer) =>
{
    using var stream = file.OpenReadStream();
    var people = await importer.ImportAsync(stream);

    return people;
});

importerApiGroup.MapPost("excel", async (IFormFile file, IFileImporter importer) =>
{
    using var stream = file.OpenReadStream();
    var people = await importer.ImportAsync(stream);

    return people;
});

app.Run();