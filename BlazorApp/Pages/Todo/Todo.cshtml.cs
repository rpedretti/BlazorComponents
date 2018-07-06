using BlazorApp.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Pages
{
    public class TodoBase : BlazorComponent
    {
        #region Properties

        public int DoneItemsCount => Items.Where(todo => !todo.IsDone).Count();
        public string HeaderA11l => $"{DoneItemsCount} de {Items.Count()} feitos";
        public IList<TodoItem> Items { get; } = new List<TodoItem>();

        #endregion Properties
    }
}
