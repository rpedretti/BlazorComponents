using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Domain;
using BlazorApp.NHContext;
using NHibernate;
using NHibernate.Linq;

namespace BlazorApp.Repository
{
    public sealed class NHTodoItemRepository : BaseNHContext, ITodoItemRepository
    {
        public NHTodoItemRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public async Task AddTodoItemAsync(TodoItem user)
        {
            await WithAutoTransaction(async s => await s.SaveAsync(user));
        }

        public async Task<TodoItem> GetTodoItemAsync(long id)
        {
            return await WithSessionAsync(async s => { return await s.GetAsync<TodoItem>(id); });
        }

        public async Task<IList<TodoItem>> GetTodoItemsAsync()
        {
            return await WithSessionAsync(async s => await s.Query<TodoItem>().ToListAsync());
        }

        public async Task RemoveTodoItemAsync(TodoItem user)
        {
            await WithAutoTransaction(async s => await s.DeleteAsync(user));
        }

        public async Task RemoveTodoItemAsync(long id)
        {
            await WithAutoTransaction(async s => {
                var user = await s.GetAsync<TodoItem>(id);
                await s.DeleteAsync(user);
            });
        }

        public async Task UpdateTodoItemAsync(TodoItem user)
        {
            await WithAutoTransaction(async s =>
            {
                await s.UpdateAsync(user);
            });
        }
    }
}
