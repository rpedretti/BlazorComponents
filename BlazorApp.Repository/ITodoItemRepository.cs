using BlazorApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Repository
{
    public interface ITodoItemRepository
    {
        Task AddTodoItemAsync(TodoItem user);
        Task<TodoItem> GetTodoItemAsync(long id);
        Task<IList<TodoItem>> GetTodoItemsAsync();
        Task RemoveTodoItemAsync(TodoItem user);
        Task RemoveTodoItemAsync(long id);
        Task UpdateTodoItemAsync(TodoItem user);
    }
}
