using System.Threading.Tasks;
using BlazorApp30.Domain;

namespace BlazorApp30.Services
{
    public interface IMovieService
    {
        Task<SearchResult> FindMoviesByPattern(string pattern, int page);
        Task<Movie> GetMovieByTitle(string title);
        Task<Movie> GetMovieById(string id);
    }
}