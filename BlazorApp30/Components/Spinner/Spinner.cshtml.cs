using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp30.Components.Spinner
{
    public class SpinnerBase : BlazorComponent
    {
        [Parameter]
        protected bool Active { get; set; }

        [Parameter]
        protected bool Centered { get; set; }
    }
}
