using System;

namespace BlazorApp.Store.Counter
{
    public class CounterState
    {
        #region Constructors

        [Obsolete("For deserialization purposes only. Use the constructor with parameters")]
        public CounterState() { }

        public CounterState(int clickCount)
        {
            ClickCount = clickCount;
        }

        #endregion Constructors

        #region Properties

        public int ClickCount { get; set; }

        #endregion Properties
    }
}
