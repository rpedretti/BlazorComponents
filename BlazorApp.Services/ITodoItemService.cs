using BlazorApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface ITodoItemService
    {
        Task AddTodoAsync(TodoItem item);
        Task UpdateTodoAsync(TodoItem item);
    }
}
