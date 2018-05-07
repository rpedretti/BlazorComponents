using System.Collections.Generic;
using System.Linq;

namespace BlazorApp30.Models
{
    public class TodoPageModel
    {
        public IList<TodoItem> Items { get; } = new List<TodoItem>();
        public int DoneItemsCount => Items.Where(todo => !todo.IsDone).Count();
        public string HeaderA11l => $"{DoneItemsCount} de {Items.Count()} feitos";
    }
}
