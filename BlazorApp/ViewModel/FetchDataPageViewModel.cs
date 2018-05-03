using BlazorApp.Domain;
using BlazorApp.Models;
using Microsoft.AspNetCore.Blazor;
using System.Net.Http;
using System.Threading.Tasks;


namespace BlazorApp.ViewModel
{
    public class FetchDataPageViewModel
    {
        private readonly HttpClient _httpClient;
        public FetchDataPageModel Model { get; set; } = new FetchDataPageModel();

        public FetchDataPageViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task FetchDataAsync()
        {
            Model.Forecasts = await _httpClient.GetJsonAsync<WeatherForecast[]>("/sample-data/weather.json");
        }
    }
}
