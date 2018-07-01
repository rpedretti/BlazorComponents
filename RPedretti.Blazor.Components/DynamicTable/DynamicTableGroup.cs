using System.Collections.Generic;

namespace RPedretti.Blazor.Components.DynamicTable
{
    public class DynamicTableGroup
    {
        #region Properties

        public bool Collapsed { get; set; }
        public List<DynamicTableRow> Rows { get; set; }
        public string Title { get; set; }

        #endregion Properties
    }
}
