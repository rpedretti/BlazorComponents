using BlazorApp.Domain;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IForecastService
    {
        #region Methods

        Task<WeatherForecast[]> GetForecastAsync();

        #endregion Methods
    }
}
