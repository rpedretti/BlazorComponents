using System;
using System.Threading;

namespace RPedretti.Blazor.Components.Operators
{
    public class DebounceDispatcher
    {
        #region Fields

        private Timer _timer;

        #endregion Fields

        #region Properties

        private DateTime _timerStarted { get; set; } = DateTime.UtcNow.AddYears(-1);

        #endregion Properties

        #region Methods

        /// <summary>
        /// Debounce reset timer and after last item recieved give you last item.
        /// <exception cref="http://demo.nimius.net/debounce_throttle/">See this example for understanding what is RateLimiting and Debounce</exception>
        /// </summary>
        /// <param name="obj">Your object</param>
        /// <param name="interval">Milisecond interval</param>
        /// <param name="debounceAction">Called when last item call this method and after interval was finished</param>
        public void Debounce(int interval, Action<object> action, object param = null)
        {
            _timer?.Dispose();
            _timer = new Timer(state =>
            {
                _timer.Dispose();
                if (_timer != null)
                {
                    action?.Invoke(param);
                }

                _timer = null;
            }, param, interval, interval);
        }

        #endregion Methods
    }
}
