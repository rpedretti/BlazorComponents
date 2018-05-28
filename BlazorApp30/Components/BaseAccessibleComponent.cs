using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp30.Components
{
    public class BaseAccessibleComponent : BlazorComponent
    {
        [Parameter] protected string A11yLabel { get; set; }

        [Parameter] protected string A11yRole { get; set; }

        [Parameter] protected int? A11yPosInSet { get; set; }

        [Parameter] protected int? A11ySetSize { get; set; }
    }
}
