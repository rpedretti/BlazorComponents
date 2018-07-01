using Blazor.Fluxor;

namespace BlazorApp40.Store.Counter
{
    public class CounterFeature : Feature<CounterState>
    {
        #region Methods

        protected override CounterState GetInitialState() => new CounterState(0);

        public override string GetName() => "Counter";

        #endregion Methods
    }
}
