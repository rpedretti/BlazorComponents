using System.Collections.Generic;

namespace BlazorApp30.Components.DynamicTable
{
    public class DynamicTableRow
    {
        public IEnumerable<DynamicTableCell> Cells { get; set; }
        public string Classes { get; set; }
    }
}
