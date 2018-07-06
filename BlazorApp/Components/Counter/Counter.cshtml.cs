using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Components
{
    public class CounterBase : BlazorComponent, IDisposable
    {
        #region Properties

        [Parameter] protected string ButtonText { get; set; }

        [Parameter] protected int CurrentCount { get; set; } = 0;

        [Parameter] protected Func<int, Task> CurrentCountChanged { get; set; }

        [Parameter] protected int IncrementAmount { get; set; } = 1;

        [Parameter] protected string Title { get; set; }

        #endregion Properties

        #region Methods

        public void Dispose()
        {
            CurrentCount -= IncrementAmount;
        }

        public async Task IncrementCountAsync()
        {
            CurrentCount += IncrementAmount;
            await CurrentCountChanged?.Invoke(CurrentCount);
        }

        #endregion Methods
    }
}
