using RPedretti.Blazor.Components.Sample.Domain;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.Sample.Services
{
    public interface IForecastService
    {
        #region Methods

        Task<WeatherForecast[]> GetForecastAsync();

        #endregion Methods
    }
}
