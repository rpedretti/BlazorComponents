﻿using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp30.Components.Spinner
{
    public class SpinnerBase : BlazorComponent
    {
        [Parameter]
        protected bool Active { get; set; }

        [Parameter]
        protected SpinnerSize Size { get; set; } = SpinnerSize.SMALL;

        [Parameter]
        protected bool Centered { get; set; }
    }

    public enum SpinnerSize
    {
        SMALL,
        MEDIUM,
        LARGE
    }
}