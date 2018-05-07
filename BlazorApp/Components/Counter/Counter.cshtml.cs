using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Components
{
    public class CounterBase : BlazorComponent
    {
        [Parameter]
        protected Action<int> CurrentCountChanged { get; set; }
        [Parameter]
        protected string Title { get; set; }
        [Parameter]
        protected string ButtonText { get; set; }
        [Parameter]
        protected int CurrentCount { get; set; } = 0;
        [Parameter]
        protected int IncrementAmount { get; set; } = 1;

        public void IncrementCount()
        {
            CurrentCount += IncrementAmount;
            CurrentCountChanged?.Invoke(CurrentCount);
        }
    }
}
