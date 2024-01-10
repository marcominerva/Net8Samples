using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WebApi.BusinessLayer.Models;
using WebApi.BusinessLayer.Services.Interfaces;

namespace WebApi.BusinessLayer.Services;

public class WeatherService(HttpClient httpClient, IMemoryCache cache, ILogger<WeatherService> logger) : IWeatherService
{
    public async Task<IEnumerable<WeatherForecast>> GetForecastAsync(string city, int days)
    {
        logger.LogInformation("Getting weather condition for {City}...", city);

        var response = await cache.GetOrCreateAsync($"forecast-{city}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4);

            var response = await httpClient.GetStringAsync($"forecast/daily?q={city}&cnt={days}");
            using var document = JsonDocument.Parse(response);

            var forecast = document.RootElement.GetProperty("list").EnumerateArray().Select(x => new WeatherForecast(
                Date: DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(x.GetProperty("dt").GetInt64()).UtcDateTime),
                TemperatureC: (int)x.GetProperty("temp").GetProperty("day").GetDouble(),
                Summary: x.GetProperty("weather")[0].GetProperty("description").GetString()
            )).ToList();

            return forecast;
        });

        return response;
    }
}
