using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.DynamicTable
{
    public class DynamicTableBase : BaseComponent
    {
        [Parameter] protected string Classes { get; set; }
        [Parameter] protected IEnumerable<DynamicTableHeader> Headers { get; set; }
        [Parameter] protected IEnumerable<DynamicTableRow> Rows { get; set; }
        [Parameter] protected bool Loading { get; set; }
        [Parameter] protected Func<string, bool, Task> SortRequest { get; set; }

        protected readonly Dictionary<DynamicTableHeader, bool> SortedTable = new Dictionary<DynamicTableHeader, bool>();
        protected DynamicTableHeader CurrentOrdered { get; set; }

        protected void Sort(DynamicTableHeader header)
        {
            if (CurrentOrdered != null && CurrentOrdered != header)
            {
                SortedTable.Remove(CurrentOrdered);
            }

            CurrentOrdered = header;

            if (!SortedTable.ContainsKey(CurrentOrdered))
            {
                SortedTable[CurrentOrdered] = true;
            }

            var isAsc = SortedTable[CurrentOrdered];

            SortRequest?.Invoke(header.SortId, isAsc);

            SortedTable[CurrentOrdered] = !SortedTable[CurrentOrdered];
        }
    }
}
