using RPedretti.Blazor.Shared.Domain;
using System.Threading.Tasks;

namespace RPedretti.Blazor.SignalRServer.Repository
{
    /// <summary>
    /// Class for handling users
    /// </summary>
    public interface IUserRepository
    {
        #region Methods

        Task<bool> AddUserAsync(User user);

        /// <summary>
        /// Searches for a user at the repository
        /// </summary>
        /// <param name="username">The username to look for</param>
        /// <returns>Returns a task wich result yields a User</returns>
        Task<User> GetUserAsync(string username);

        Task<bool> RemoveUserAsync(string username);

        #endregion Methods
    }
}
