using System.Threading.Tasks;
using RPedretti.Blazor.Shared.Domain;

namespace RPedretti.Blazor.SignalRServer.Repository
{
    /// <summary>
    /// Class for handling users
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Searches for a user at the repository
        /// </summary>
        /// <param name="username">The username to look for</param>
        /// <returns>Returns a task wich result yields a User</returns>
        Task<User> GetUserAsync(string username);

        Task<bool> AddUserAsync(User user);

        Task<bool> RemoveUserAsync(string username);
    }
}
