using Microsoft.AspNetCore.Blazor;
using System;

namespace RPedretti.Blazor.Components.Layout.DynamicTable
{
    public class DynamicTableColumn<T>
    {
        #region Properties

        public string Classes { get; set; }
        public RenderFragment<T> Template { get; set; }
        public string SortProp { get; set; }

        #endregion Properties
    }
}
