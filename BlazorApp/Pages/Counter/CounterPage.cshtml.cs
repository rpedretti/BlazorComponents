using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public class CounterPageBase : BaseComponent
    {
        #region Fields

        protected int IncrementAmount = 1;

        #endregion Fields

        #region Properties

        protected string CounterIncrementDescription { get; set; }

        protected string CounterTitle { get; set; }

        #endregion Properties

        #region Methods

        protected void CounterChanged(int v)
        {
            CurrentCount++;
        }

        #endregion Methods

        #region Constructors

        public CounterPageBase()
        {
            CounterTitle = "My counter";
            CounterIncrementDescription = $"Add {IncrementAmount}";
        }

        #endregion Constructors

        public int CurrentCount { get; set; }
    }
}
