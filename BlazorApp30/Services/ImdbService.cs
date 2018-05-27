using BlazorApp30.Domain;
using Microsoft.AspNetCore.Blazor;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp30.Services
{
    public sealed class ImdbService : IMovieService
    {
        private const string key = "5cea5c6";
        private const string _baseUrl = "http://www.omdbapi.com/?apikey=" + key;
        private readonly HttpClient httpClient;

        public ImdbService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SearchResult> FindMoviesByPattern(string pattern, int page)
        {
            var movies = await httpClient.GetJsonAsync<SearchResult>($"{_baseUrl}&s={pattern}&page={page}");
            return movies;
        }

        public async Task<Movie> GetMovieById(string id)
        {
            await Task.Delay(1500);
            var movie = await httpClient.GetJsonAsync<Movie>($"{_baseUrl}&i={id}");
            return movie;
        }

        public async Task<Movie> GetMovieByTitle(string title)
        {
            await Task.Delay(1500);
            var movie = await httpClient.GetJsonAsync<Movie>($"{_baseUrl}&t={title}");
            return movie;
        }
    }
}
