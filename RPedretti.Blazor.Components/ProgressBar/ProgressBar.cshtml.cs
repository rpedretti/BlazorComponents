using Microsoft.AspNetCore.Blazor.Components;

namespace RPedretti.Blazor.Components.ProgressBar
{
    public class ProgressBarBase : BlazorComponent
    {
        #region Properties

        [Parameter] protected bool Active { get; set; }

        #endregion Properties
    }
}
