using Blazor.Fluxor;

namespace BlazorApp.Store.Counter
{
    public class IncrementCounterReducer : Reducer<CounterState, IAction>
    {
        #region Methods

        public override CounterState Reduce(CounterState state, IAction action)
        {
            switch (action)
            {
                case IncrementCounterAction ica:
                    return new CounterState(state.ClickCount + 1);

                default:
                    return state;
            }
        }

        #endregion Methods
    }
}
