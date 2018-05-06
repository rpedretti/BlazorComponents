using BlazorApp.Domain;
using FluentNHibernate.Mapping;

namespace BlazorApp.NHContext.Mappings
{
    public class TodoItemMap : ClassMap<TodoItem>
    {
        public TodoItemMap()
        {
            Id(t => t.Id);
            Map(t => t.IsDone);
            Map(t => t.Title);
        }
    }
}
