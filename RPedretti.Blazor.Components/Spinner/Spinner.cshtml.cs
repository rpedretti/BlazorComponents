using Microsoft.AspNetCore.Blazor.Components;

namespace RPedretti.Blazor.Components.Spinner
{
    public enum SpinnerSize
    {
        EXTRA_SMALL,
        SMALL,
        MEDIUM,
        LARGE
    }

    public class SpinnerBase : BlazorComponent
    {
        #region Properties

        [Parameter] protected bool Active { get; set; }

        [Parameter] protected bool Centered { get; set; }
        [Parameter] protected SpinnerSize Size { get; set; } = SpinnerSize.SMALL;

        #endregion Properties
    }
}
