using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Sensors.AmbientLight
{
    public class AmbientLightSensor : IDisposable
    {
        #region Fields

        private DotNetObjectRef ambientLightSensorRef;

        public string Error { get; private set; }

        #endregion Fields

        #region Methods

        internal void Init()
        {
            ambientLightSensorRef = new DotNetObjectRef(this);
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorSensors.lightsensor.initSensor", ambientLightSensorRef);
        }

        #endregion Methods

        #region Events

        private event EventHandler<string> _onError;
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

        public event EventHandler<int> _onReading;
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

        #endregion Events

        public void Dispose()
        {
            ambientLightSensorRef.Dispose();
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
