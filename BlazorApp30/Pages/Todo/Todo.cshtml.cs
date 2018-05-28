using BlazorApp30.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp30.Pages
{
    public class TodoBase : BlazorComponent
    {
        public IList<TodoItem> Items { get; } = new List<TodoItem>();
        public int DoneItemsCount => Items.Where(todo => !todo.IsDone).Count();
        public string HeaderA11l => $"{DoneItemsCount} de {Items.Count()} feitos";
    }
}
