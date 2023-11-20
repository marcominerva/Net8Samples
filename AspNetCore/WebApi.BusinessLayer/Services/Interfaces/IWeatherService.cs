using WebApi.BusinessLayer.Models;

namespace WebApi.BusinessLayer.Services.Interfaces;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecast>> GetForecastAsync(string city, int days);
}
