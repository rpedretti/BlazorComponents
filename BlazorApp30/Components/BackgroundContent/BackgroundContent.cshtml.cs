using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Drawing;

namespace BlazorApp30.Components.BackgroundContent
{
    public class BackgroundContentBase : BlazorComponent
    {
        [Parameter] protected BackgroundContentType Type { get; set; } = BackgroundContentType.SUCCESS;

        [Parameter] protected RenderFragment ChildContent { get; set; }

        [Parameter] protected Color ParentBgColor { get; set; } = Color.Transparent;

        [Parameter] protected Color ChildBgColor { get; set; } = Color.Transparent;

        protected string _id { get; private set; }
        protected string ComponentStyle { get; private set; }

        public BackgroundContentBase()
        {
            _id = $"_{Guid.NewGuid().ToString().Replace("-", "")}";
        }
    }
}
