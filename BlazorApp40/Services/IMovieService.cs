using System.Threading;
using System.Threading.Tasks;
using BlazorApp40.Domain;

namespace BlazorApp40.Services
{
    public interface IMovieService
    {
        Task<MovieSearchResult> FindMoviesByPattern(string pattern, int page, CancellationToken cancelationToken);
        Task<MovieSearchResult> FindMoviesByPattern(string pattern, int page);
        Task<Movie> GetMovieByTitle(string title, CancellationToken cancelationToken);
        Task<Movie> GetMovieByTitle(string title);
        Task<Movie> GetMovieById(string id, CancellationToken cancelationToken);
        Task<Movie> GetMovieById(string id);
    }
}