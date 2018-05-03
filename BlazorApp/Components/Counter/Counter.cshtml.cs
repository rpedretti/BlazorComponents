using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace BlazorApp.Components
{
    public class CounterBase : BlazorComponent
    {
        public Action<int> CurrentCountChanged { get; set; }
        public string Title { get; set; }
        public string ButtonText { get; set; }
        public int CurrentCount { get; set; } = 0;
        public int IncrementAmount { get; set; } = 1;

        public void IncrementCount()
        {
            CurrentCount += IncrementAmount;
            CurrentCountChanged?.Invoke(CurrentCount);
        }
    }
}
