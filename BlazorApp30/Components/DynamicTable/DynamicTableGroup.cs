using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Components.DynamicTable
{
    public class DynamicTableGroup
    {
        public string Title { get; set; }
        public List<DynamicTableRow> Rows { get; set; }
        public bool Collapsed { get; set; }
    }
}
