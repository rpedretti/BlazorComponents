using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorApp.Domain;
using BlazorApp.Repository;

namespace BlazorApp.Services
{
    public sealed class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task AddTodoAsync(TodoItem item)
        {
            await _todoItemRepository.AddTodoItemAsync(item);
        }

        public async Task UpdateTodoAsync(TodoItem item)
        {
            await _todoItemRepository.UpdateTodoItemAsync(item);
        }
    }
}
