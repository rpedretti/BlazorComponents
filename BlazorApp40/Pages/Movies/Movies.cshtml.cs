using BlazorApp40.Models;
using BlazorApp40.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp40.Pages.Movies
{
    public class MoviesBase : BlazorComponent
    {
        [Inject]
        private IMovieService _movieService { get; set; }
        private Dictionary<int, IEnumerable<MoviePosterModel>> CachedMovies = new Dictionary<int, IEnumerable<MoviePosterModel>>();
        private CancellationTokenSource requestToken;

        protected List<MoviePosterModel> Movies { get; set; } = new List<MoviePosterModel>();
        protected string SearchMovieTitle { get; set; }
        protected int MoviesCount { get; set; }
        protected int CurrentPage { get; set; }
        protected int PageCount { get; set; }
        protected bool Loading { get; set; }
        protected bool HasContent { get; set; }

        protected async void RequestPage(int page)
        {
            await GetMoviesAsync(page);
            StateHasChanged();
        }

        public void ClearMovies()
        {
            Movies.Clear();
            MoviesCount = 0;
            HasContent = false;
            CachedMovies.Clear();
        }

        public async Task SearchAsync()
        {
            ClearMovies();
            await GetMoviesAsync();
        }

        public void GoToMovie(string id)
        {
            Console.WriteLine($"Olar filme {id}");
        }

        public async Task GetMoviesAsync(int page = 1)
        {
            IEnumerable<MoviePosterModel> movies;

            Loading = true;

            Movies.Clear();
            CurrentPage = page;

            StateHasChanged();

            if (!CachedMovies.ContainsKey(page))
            {
                try
                {
                    if (requestToken != null)
                    {
                        requestToken.Cancel();
                    }

                    requestToken = new CancellationTokenSource();

                    var moviesResult = await _movieService.FindMoviesByPattern(SearchMovieTitle, page, requestToken.Token);

                    if (moviesResult.Response)
                    {
                        movies = moviesResult.Search.Select(m => new MoviePosterModel
                        {
                            Id = m.imdbID,
                            Plot = m.Plot,
                            Poster = m.Poster,
                            Title = m.Title
                        });

                        CachedMovies[page] = movies;
                        if (MoviesCount == 0)
                        {
                            MoviesCount = moviesResult.totalResults;
                            HasContent = MoviesCount > 0;
                            PageCount = (int)Math.Ceiling(MoviesCount / 10d);
                        }

                        movies = CachedMovies[page];
                        Movies.AddRange(movies);
                    }
                    else
                    {
                        HasContent = false;
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Task cancelled");
                }
                catch
                {
                    HasContent = false;
                }
            }
            else
            {
                movies = CachedMovies[page];
                Movies.AddRange(movies);
            }

            Loading = false;
            StateHasChanged();
        }
    }
}
