using System.Collections.Generic;

namespace RPedretti.Blazor.Components.DynamicTable
{
    public class DynamicTableRow
    {
        #region Properties

        public IEnumerable<DynamicTableCell> Cells { get; set; }
        public string Classes { get; set; }

        #endregion Properties
    }
}
