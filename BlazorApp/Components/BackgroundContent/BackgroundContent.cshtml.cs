using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Components.BackgroundContent
{
    public class BackgroundContentBase : BlazorComponent
    {
        public RenderFragment ChildContent { get; set; }
        public Color BgColor { get; set; } = Color.Transparent;
    }
}
