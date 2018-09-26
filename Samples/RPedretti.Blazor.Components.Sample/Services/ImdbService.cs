using Microsoft.JSInterop;
using RPedretti.Blazor.Components.Sample.Domain;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Blazor.Extensions.Logging;

namespace RPedretti.Blazor.Components.Sample.Services
{
    public sealed class ImdbService : IMovieService
    {
        #region Fields

        private const string _baseUrl = "https://www.omdbapi.com/?apikey=" + key;

        private const string key = "5cea5c6";

        private readonly HttpClient httpClient;
        private readonly ILogger<ImdbService> logger;

        #endregion Fields

        #region Constructors

        public ImdbService(HttpClient httpClient, ILogger<ImdbService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        #endregion Constructors

        #region Methods

        public async Task<MovieSearchResult> FindMoviesByPattern(string pattern, int page)
        {
            return await FindMoviesByPattern(pattern, page, CancellationToken.None);
        }

        public async Task<MovieSearchResult> FindMoviesByPattern(string pattern, int page, CancellationToken cancelationToken)
        {
            try
            {
                var responseJson = await httpClient.GetAsync($"{_baseUrl}&s={pattern}&page={page}", cancelationToken);
                var content = await responseJson.Content.ReadAsStringAsync();
                var movies = Json.Deserialize<MovieSearchResult>(content);

                return movies;
            }
            catch (Exception e)
            {
                logger.LogError(e);
                throw;
            }
        }

        public async Task<Movie> GetMovieById(string id)
        {
            return await GetMovieById(id, CancellationToken.None);
        }

        public async Task<Movie> GetMovieById(string id, CancellationToken cancelationToken)
        {
            var responseJson = await httpClient.GetAsync($"{_baseUrl}&i={id}");
            var content = await responseJson.Content.ReadAsStringAsync();

            var movie = Json.Deserialize<Movie>(content);
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

            var movie = Json.Deserialize<Movie>(content);
            return movie;
        }

        #endregion Methods
    }
}
