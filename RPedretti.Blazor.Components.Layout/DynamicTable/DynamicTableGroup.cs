using System.Collections.Generic;

namespace RPedretti.Blazor.Components.Layout.DynamicTable
{
    public class DynamicTableGroup<T>
    {
        #region Properties

        public string Classes { get; set; }
        public bool Collapsed { get; set; }
        public List<DynamicTableRow<T>> Rows { get; set; }

        #endregion Properties
    }
}
