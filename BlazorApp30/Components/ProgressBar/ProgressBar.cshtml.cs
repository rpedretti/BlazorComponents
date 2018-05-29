using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp30.Components.ProgressBar
{
    public class ProgressBarBase: BlazorComponent
    {
        [Parameter] protected bool Active { get; set; }
    }
}
