using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Components.DynamicTable
{
    public class DynamicTableBase: BlazorComponent
    {
        [Parameter] protected string Classes { get; set; }

        [Parameter] protected IEnumerable<DynamicTableHeader> Headers { get; set; }

        [Parameter] protected IEnumerable<DynamicTableRow> Rows { get; set; }

        [Parameter] protected bool Loading { get; set; }

        protected readonly Dictionary<DynamicTableHeader, bool> SortedTable = new Dictionary<DynamicTableHeader, bool>();
        protected DynamicTableHeader CurrentOrdered { get; set; }
        protected void HandleKeyPress(UIKeyboardEventArgs args, Action action)
        {
            if (args.Key == " " || args.Key == "Enter")
            {
                action?.Invoke();
            }
        }

        protected void Sort(DynamicTableHeader header)
        {
            if (CurrentOrdered != null && CurrentOrdered != header)
            {
                SortedTable.Remove(CurrentOrdered);
            }

            CurrentOrdered = header;

            var index = Headers.ToList().IndexOf(CurrentOrdered);

            if (!SortedTable.ContainsKey(CurrentOrdered))
            {
                SortedTable[CurrentOrdered] = true;
            }

            var isAsc = SortedTable[CurrentOrdered];

            if (isAsc)
            {
                Rows = Rows.OrderBy(r => r.Cells.ElementAt(index).Content);
            }
            else
            {
                Rows = Rows.OrderByDescending(r => r.Cells.ElementAt(index).Content);
            }

            SortedTable[CurrentOrdered] = !SortedTable[CurrentOrdered];
        }
    }
}
