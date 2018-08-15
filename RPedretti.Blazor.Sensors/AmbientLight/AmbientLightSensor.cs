using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Sensors.AmbientLight
{
    public class AmbientLightSensor : IDisposable
    {
        #region Fields

        private DotNetObjectRef ambientLightSensorRef;

        #endregion Fields

        #region Methods

        internal void Init()
        {
            ambientLightSensorRef = new DotNetObjectRef(this);
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorSensors.lightsensor.initSensor", ambientLightSensorRef);
        }

        #endregion Methods

        #region Events

        public event EventHandler<object> OnError;

        public event EventHandler<int> OnReading;

        #endregion Events

        public void Dispose()
        {
            ambientLightSensorRef.Dispose();
        }

        [JSInvokable]
        public Task NotifyError(object error)
        {
            OnError?.Invoke(this, error);
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task NotifyReading(int illuminance)
        {
            OnReading?.Invoke(this, illuminance);
            return Task.CompletedTask;
        }
    }
}
