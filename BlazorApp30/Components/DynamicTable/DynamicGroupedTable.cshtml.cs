using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;

namespace BlazorApp30.Components.DynamicTable
{
    public class DynamicGroupedTableBase : BaseComponent
    {
        [Parameter] protected string Classes { get; set; }

        [Parameter] protected List<DynamicTableHeader> Headers { get; set; }

        [Parameter] protected List<DynamicTableGroup> Groups { get; set; }

        [Parameter] protected bool Loading { get; set; }
        
        protected void ToggleGroupCollapsed(DynamicTableGroup group)
        {
            group.Collapsed = !group.Collapsed;
        }
    }
}
