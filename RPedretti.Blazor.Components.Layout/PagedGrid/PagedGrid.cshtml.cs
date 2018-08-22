using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components.Layout.Pager;
using System;

namespace RPedretti.Blazor.Components.Layout.PagedGrid
{
    public class PagedGridBase : BaseAccessibleComponent
    {
        #region Properties

        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected int CurrentPage { get; set; }
        [Parameter] protected bool HasContent { get; set; }
        [Parameter] protected bool Loading { get; set; }
        [Parameter] protected int MaxIndicators { get; set; }
        [Parameter] protected string NoContentMessage { get; set; } = "No content";
        [Parameter] protected Action<int> OnRequestPage { get; set; }
        [Parameter] protected int PageCount { get; set; }
        [Parameter] protected PagerPosition PagerPosition { get; set; } = PagerPosition.CENTER;
        [Parameter] protected bool SmallPager { get; set; }

        #endregion Properties
    }
}
