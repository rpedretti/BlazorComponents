using BlazorApp.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components;
using System;
using System.Collections.Generic;

namespace BlazorApp.Components
{
    public class TodoListBase : BaseComponent
    {
        #region Properties

        [Parameter] protected string Id { get; set; }
        [Parameter] protected IList<TodoItem> Items { get; set; }
        [Parameter] protected Action OnItemsChanged { get; set; }

        private string _previousTitle;
        private string _newTodoTitle;
        protected string NewTodoTitle
        {
            get => _newTodoTitle;
            set
            {
                _previousTitle = _newTodoTitle;
                _newTodoTitle = value;
            }
        }

        #endregion Properties

        #region Methods

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

        protected override bool ShouldRender()
        {
            return string.IsNullOrEmpty(_previousTitle) && !string.IsNullOrEmpty(_newTodoTitle)
                || string.IsNullOrEmpty(_newTodoTitle) && !string.IsNullOrEmpty(_previousTitle);
        }

        #endregion Methods
    }
}
