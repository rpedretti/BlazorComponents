using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BlazorApp30.Components.BackgroundContent
{
    public class BackgroundContentBase : BlazorComponent
    {
        [Parameter]
        protected RenderFragment ChildContent { get; set; }

        [Parameter]
        protected Color ParentBgColor { get; set; }

        [Parameter]
        protected Color ChildBgColor { get; set; }
    }
}
