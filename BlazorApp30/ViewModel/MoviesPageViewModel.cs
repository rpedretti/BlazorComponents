using BlazorApp30.Models;
using BlazorApp30.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.ViewModel
{
    public sealed class MoviesPageViewModel : BaseViewModel
    {
        private readonly IMovieService _movieService;
        private Dictionary<int, IEnumerable<MoviePosterModel>> CachedMovies = new Dictionary<int, IEnumerable<MoviePosterModel>>();

        public MoviesPageModel Model { get; private set; } = new MoviesPageModel();
        public bool Loading { get; set; }
        public bool HasContent { get; set; }

        public MoviesPageViewModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public void ClearMovies()
        {
            Model.Movies.Clear();
        }

        public void GoToMovie(string id)
        {
            Console.WriteLine($"Olar filme {id}");
        }

        public async Task GetMoviesAsync(int page = 1)
        {
            IEnumerable<MoviePosterModel> movies;

            Loading = true;

            Model.Movies.Clear();
            Model.CurrentPage = page;

            OnStateHasChanged();

            if (!CachedMovies.ContainsKey(page))
            {
                var moviesResult = await _movieService.FindMoviesByPattern("avengers", page);
                movies = moviesResult.Search.Select(m => new MoviePosterModel
                {
                    Id = m.imdbID,
                    Plot = m.Plot,
                    Poster = m.Poster,
                    Title = m.Title
                });

                CachedMovies[page] = movies;
                if (Model.MoviesCount == 0)
                {
                    Model.MoviesCount = moviesResult.totalResults;
                    HasContent = Model.MoviesCount > 0;
                    Model.PageCount = (int)Math.Ceiling(Model.MoviesCount / 10d);
                }
            }

            movies = CachedMovies[page];

            Model.Movies.AddRange(movies);


            Loading = false;

            OnStateHasChanged();
        }
    }
}
