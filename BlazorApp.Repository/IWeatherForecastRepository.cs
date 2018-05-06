using BlazorApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Repository
{
    public interface IWeatherForecastRepository
    {
        Task SaveTodoItemAsync(WeatherForecast forecast);
        Task<WeatherForecast> GetWeatherForecastsAsync();
    }
}
