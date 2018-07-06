using Blazor.Fluxor;

namespace BlazorApp.Store.Counter
{
    public class CounterFeature : Feature<CounterState>
    {
        #region Methods

        protected override CounterState GetInitialState() => new CounterState(0);

        public override string GetName() => "Counter";

        #endregion Methods
    }
}
