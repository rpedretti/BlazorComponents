using System.Collections.Generic;

namespace RPedretti.Blazor.Components.DynamicTable
{
    public class DynamicTableGroup
    {
        public string Title { get; set; }
        public List<DynamicTableRow> Rows { get; set; }
        public bool Collapsed { get; set; }
    }
}
