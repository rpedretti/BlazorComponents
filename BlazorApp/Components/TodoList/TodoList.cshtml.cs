using BlazorApp.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Components
{
    public class TodoListBase : BlazorComponent
    {
        [Parameter]
        protected Action OnItemsChanged { get; set; }
        [Parameter]
        protected IList<TodoItem> Items { get; set; }
        [Parameter]
        protected string Id { get; set; }

        protected string NewTodoTitle { get; set; }

        protected void AddTodo()
        {
            Items.Add(new TodoItem { Title = NewTodoTitle, IsDone = false });
            NewTodoTitle = string.Empty;
            OnItemsChanged?.Invoke();
        }

        protected void Checked(UIChangeEventArgs a, TodoItem item)
        {
            item.IsDone = (bool)a.Value;
            OnItemsChanged?.Invoke();
        }

        protected void TitleChanged(UIChangeEventArgs a, TodoItem item)
        {
            item.Title = a.Value as string;
            OnItemsChanged?.Invoke();
        }

        protected void OnInputChanged(UIChangeEventArgs a)
        {
            NewTodoTitle = a.Value as string;
        }
    }
}
