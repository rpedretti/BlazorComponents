using Microsoft.AspNetCore.Blazor.Components;
using System.Collections.Generic;

namespace RPedretti.Blazor.Components.Layout.DynamicTable
{
    public class DynamicGroupedTableBase : BaseComponent
    {
        #region Properties

        [Parameter] protected string Classes { get; set; }

        [Parameter] protected List<DynamicTableHeader> Headers { get; set; }
        [Parameter] protected bool Loading { get; set; }

        #endregion Properties
    }
}
