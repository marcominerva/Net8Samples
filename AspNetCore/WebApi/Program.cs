using Microsoft.AspNetCore.Mvc;
using TinyHelpers.AspNetCore.Extensions;
using TinyHelpers.Http;
using WebApi.BusinessLayer.Services;
using WebApi.BusinessLayer.Services.Interfaces;
using WebApi.BusinessLayer.Settings;
using WebApi.Handlers;

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

builder.Services.AddKeyedScoped<IFileImporter, CsvImporter>("csv");
builder.Services.AddKeyedScoped<IFileImporter, ExcelImporter>("excel");

builder.Services.AddProblemDetails();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseStatusCodePages();

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

//importerApiGroup.MapPost("csv", async (IFormFile file, [FromKeyedServices("csv")] IFileImporter importer) =>
//{
//    using var stream = file.OpenReadStream();
//    var people = await importer.ImportAsync(stream);

//    return people;
//});

//importerApiGroup.MapPost("excel", async (IFormFile file, [FromKeyedServices("excel")] IFileImporter importer) =>
//{
//    using var stream = file.OpenReadStream();
//    var people = await importer.ImportAsync(stream);

//    return people;
//});

importerApiGroup.MapPost("{type}", async (IFormFile file, string type, IServiceProvider provider) =>
{
    var importer = provider.GetRequiredKeyedService<IFileImporter>(type);

    using var stream = file.OpenReadStream();
    var people = await importer.ImportAsync(stream);

    return people;
});

app.MapPost("/api/products", ([FromForm] Product product) =>
{
    return TypedResults.Ok(product);
})
.DisableAntiforgery()
.WithOpenApi();

app.MapGet("/api/timeouts/{timeout:int}", async (int timeout = 100) =>
{
    await Task.Delay(timeout);
    return TypedResults.NoContent();
})
.WithOpenApi();

app.MapGet("/api/exception", () => { throw new Exception("Unexpected error"); });
app.MapGet("/api/appexception", () => { throw new ApplicationException("Application error"); });

app.Run();

public record class Product(string Name, double Price);