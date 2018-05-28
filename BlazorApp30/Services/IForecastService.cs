using BlazorApp30.Domain;
using System.Threading.Tasks;

namespace BlazorApp30.Services
{
    public interface IForecastService
    {
        Task<WeatherForecast[]> GetForecastAsync();
    }
}