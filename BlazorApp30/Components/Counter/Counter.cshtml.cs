using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace BlazorApp30.Components
{
    public class CounterBase : BlazorComponent, IDisposable
    {
        [Parameter] protected int CurrentCount { get; set; } = 0;
        [Parameter] protected Action<int> CurrentCountChanged { get; set; }

        [Parameter] protected string Title { get; set; }
        [Parameter] protected string ButtonText { get; set; }
        [Parameter] protected int IncrementAmount { get; set; } = 1;

        public void Dispose()
        {
            CurrentCount -= IncrementAmount;
        }

        public void IncrementCount()
        {
            CurrentCount += IncrementAmount;
            CurrentCountChanged?.Invoke(CurrentCount);
        }
    }
}
