using BlazorApp40.Domain;
using System.Threading.Tasks;

namespace BlazorApp40.Services
{
    public interface IForecastService
    {
        #region Methods

        Task<WeatherForecast[]> GetForecastAsync();

        #endregion Methods
    }
}
