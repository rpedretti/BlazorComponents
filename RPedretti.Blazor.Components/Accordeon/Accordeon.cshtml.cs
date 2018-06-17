using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Threading;

namespace RPedretti.Blazor.Components.Accordeon
{
    public class AccordeonBase : BaseComponent
    {
        protected bool showChildren;
        [Parameter] protected string Title { get; set; }
        [Parameter] protected bool CenterTitle { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        private bool _expanded;
        [Parameter] protected bool Expanded
        {
            get => _expanded;
            set => SetParameter(ref _expanded, value, () => {
                var delay = !value ? 600 : 0;
                new Timer(_ =>
                {
                    ExpandedChanged?.Invoke(_expanded);
                    showChildren = value;
                    StateHasChanged();
                }, null, delay, Timeout.Infinite);
            });
            
        }

        [Parameter] protected Action<bool> ExpandedChanged { get; set; }
    }
}
