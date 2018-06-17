namespace RPedretti.Blazor.Components.DynamicTable
{
    public class DynamicTableHeader
    {
        public string Title { get; set; }
        public bool CanSort { get; set; }
        public string SortId { get; set; }
        public bool Hidden { get; set; }
        public string Classes { get; set; }
    }
}
