using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace BlazorApp30.Components.PagedGrid
{
    public class PagedGridBase : BaseAccessibleComponent
    {
        [Parameter]
        protected Action<int> OnRequestPage { get; set; }

        [Parameter]
        protected int MaxIndicators { get; set; }

        [Parameter]
        protected bool SmallPager { get; set; }

        [Parameter]
        protected int CurrentPage { get; set; }

        [Parameter]
        protected int PageCount { get; set; }

        [Parameter]
        protected bool Loading { get; set; }

        [Parameter]
        protected bool HasContent { get; set; }

        [Parameter]
        protected string NoContentMessage { get; set; } = "No content";

        [Parameter]
        protected string LoadingMessage { get; set; } = "Loading";

        [Parameter]
        protected RenderFragment ChildContent { get; set; }
    }
}
