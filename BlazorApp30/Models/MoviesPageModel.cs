using System.Collections.Generic;

namespace BlazorApp30.Models
{
    public sealed class MoviesPageModel
    {
        public List<MoviePosterModel> Movies { get; set; } = new List<MoviePosterModel>();
        public string SearchMovieTitle { get; set; }
        public int MoviesCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
