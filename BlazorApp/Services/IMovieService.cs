using BlazorApp.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IMovieService
    {
        #region Methods

        Task<MovieSearchResult> FindMoviesByPattern(string pattern, int page, CancellationToken cancelationToken);

        Task<MovieSearchResult> FindMoviesByPattern(string pattern, int page);

        Task<Movie> GetMovieById(string id, CancellationToken cancelationToken);

        Task<Movie> GetMovieById(string id);

        Task<Movie> GetMovieByTitle(string title, CancellationToken cancelationToken);

        Task<Movie> GetMovieByTitle(string title);

        #endregion Methods
    }
}
