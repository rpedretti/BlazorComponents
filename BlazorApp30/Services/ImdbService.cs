using BlazorApp30.Domain;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Net.Http;
using System.Threading;
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

        public async Task<MovieSearchResult> FindMoviesByPattern(string pattern, int page)
        {
            return await FindMoviesByPattern(pattern, page, CancellationToken.None);
        }

        public async Task<MovieSearchResult> FindMoviesByPattern(string pattern, int page, CancellationToken cancelationToken)
        {
            var responseJson = await httpClient.GetAsync($"{_baseUrl}&s={pattern}&page={page}", cancelationToken);
            var content = await responseJson.Content.ReadAsStringAsync();
            var movies = JsonUtil.Deserialize<MovieSearchResult>(content);

            return movies;
        }

        public async Task<Movie> GetMovieById(string id)
        {
            return await GetMovieById(id, CancellationToken.None);
        }

        public async Task<Movie> GetMovieById(string id, CancellationToken cancelationToken)
        {
            var responseJson = await httpClient.GetAsync($"{_baseUrl}&i={id}");
            var content = await responseJson.Content.ReadAsStringAsync();

            var movie = JsonUtil.Deserialize<Movie>(content);
            return movie;
        }

        public async Task<Movie> GetMovieByTitle(string title)
        {
            return await GetMovieByTitle(title, CancellationToken.None);
        }

        public async Task<Movie> GetMovieByTitle(string title, CancellationToken cancelationToken)
        {
            var responseJson = await httpClient.GetAsync($"{_baseUrl}&t={title}");
            var content = await responseJson.Content.ReadAsStringAsync();

            var movie = JsonUtil.Deserialize<Movie>(content);
            return movie;
        }
    }
}
