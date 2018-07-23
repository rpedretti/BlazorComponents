using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPedretti.Blazor.Shared.Domain;

namespace RPedretti.Blazor.SignalRServer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> users = new List<User>();

        public Task<bool> AddUserAsync(User user)
        {
            users.Add(user);
            return Task.FromResult(true);
        }

        public Task<User> GetUserAsync(string username)
        {
            return Task.FromResult(users.FirstOrDefault(u => u.Username == username));
        }

        public Task<bool> RemoveUserAsync(string username)
        {
            return Task.FromResult(users.RemoveAll(u => u.Username == username) > 0);
        }
    }
}
