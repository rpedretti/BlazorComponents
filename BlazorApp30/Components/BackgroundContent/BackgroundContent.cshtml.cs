using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Components.BackgroundContent
{
    public class BackgroundContentBase : BlazorComponent
    {
        [Parameter]
        protected RenderFragment ChildContent { get; set; }

        [Parameter]
        protected Color BgColor { get; set; } = Color.Transparent;
    }
}
