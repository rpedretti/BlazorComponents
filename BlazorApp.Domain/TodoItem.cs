namespace BlazorApp.Domain
{
    public class TodoItem
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual bool IsDone { get; set; }
    }
}
