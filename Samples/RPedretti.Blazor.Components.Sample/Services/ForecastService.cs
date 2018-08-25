using Microsoft.AspNetCore.Blazor;
using RPedretti.Blazor.Components.Sample.Domain;
using System.Net.Http;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.Sample.Services
{
    public class ForecastService : IForecastService
    {
        #region Fields

        private readonly HttpClient httpClient;

        #endregion Fields

        #region Constructors

        public ForecastService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        #endregion Constructors

        #region Methods

        public async Task<WeatherForecast[]> GetForecastAsync()
        {
            return await httpClient.GetJsonAsync<WeatherForecast[]>("/sample-data/weather.json"); ;
        }

        #endregion Methods
    }
}
