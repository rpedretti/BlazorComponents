using Blazor.Fluxor;
using BlazorApp.Store.Counter;
using Microsoft.AspNetCore.Blazor.Components;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public class CounterPageBase : BaseBlazorPage
    {
        #region Fields

        protected int IncrementAmount = 1;

        #endregion Fields

        #region Properties

        protected string CounterIncrementDescription { get; set; }

        protected string CounterTitle { get; set; }

        [Inject] protected IDispatcher Dispatcher { get; set; }

        [Inject] protected IState<CounterState> State { get; set; }

        #endregion Properties

        #region Methods

        protected async Task CounterChanged(int v)
        {
            await Dispatcher.DispatchAsync(new IncrementCounterAction());
        }

        #endregion Methods

        #region Constructors

        public CounterPageBase()
        {
            CounterTitle = "My counter";
            CounterIncrementDescription = $"Add {IncrementAmount}";
        }

        #endregion Constructors

        public int CurrentCount => State.Value.ClickCount;
    }
}
