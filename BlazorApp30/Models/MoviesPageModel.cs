using BlazorApp30.Domain;
using System.Collections.Generic;

namespace BlazorApp30.Models
{
    public sealed class MoviesPageModel
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public int MoviesCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
