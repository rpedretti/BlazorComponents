using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Sensors.AmbientLight
{
    public class AmbientLightSensor : IDisposable
    {
        #region Fields

        private DotNetObjectRef thisRef;

        #endregion Fields

        #region Events

        private event EventHandler<string> _onError;

        #endregion Events

        #region Methods

        internal void Init()
        {
            thisRef = new DotNetObjectRef(this);
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorSensors.lightsensor.initSensor", thisRef);
        }

        #endregion Methods

        #region Properties

        public string Error { get; private set; }

        #endregion Properties

        public event EventHandler<int> _onReading;

        public event EventHandler<string> OnError
        {
            add
            {
                _onError += value;
                if (Error != null)
                {
                    value.Invoke(this, Error);
                }
            }
            remove
            {
                _onError -= value;
            }
        }

        public event EventHandler<int> OnReading
        {
            add
            {
                _onReading += value;
                if (Error != null)
                {
                    _onError?.Invoke(this, Error);
                }
            }
            remove
            {
                _onReading -= value;
            }
        }

        public void Dispose()
        {
            if (thisRef != null)
            {
                JSRuntime.Current.UntrackObjectRef(thisRef);
            }
        }

        [JSInvokable]
        public Task NotifyError(string error)
        {
            Error = error;
            _onError?.Invoke(this, error);
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task NotifyReading(int illuminance)
        {
            _onReading?.Invoke(this, illuminance);
            return Task.CompletedTask;
        }
    }
}
