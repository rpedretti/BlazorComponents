using BlazorApp40.Domain;
using Microsoft.AspNetCore.Blazor;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp40.Services
{
    public class ForecastService : IForecastService
    {
        private readonly HttpClient httpClient;

        public ForecastService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<WeatherForecast[]> GetForecastAsync()
        {
            return await httpClient.GetJsonAsync<WeatherForecast[]>("/sample-data/weather.json"); ;
        }
    }
}
