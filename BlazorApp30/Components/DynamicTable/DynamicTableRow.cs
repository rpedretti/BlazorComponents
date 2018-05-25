using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Components.DynamicTable
{
    public class DynamicTableRow
    {
        public IEnumerable<DynamicTableCell> Cells { get; set; }
        public string Classes { get; set; }
    }
}
