using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Sensors.Geolocation
{
    public class GeolocationSensor
    {
        #region Fields

        private object _lock = new Object();
        private bool init;
        private DotNetObjectRef thisRef;

        private int? watchId;

        #endregion Fields

        #region Events

        private event EventHandler<PositionError> _onPositionError;

        private event EventHandler<Position> _onPositionUpdate;

        #endregion Events

        #region Methods

        private void AssureInit()
        {
            lock (_lock)
            {
                if (!init)
                {
                    init = true;
                    StartWatch();
                }
            }
        }

        private void AssureStop()
        {
            lock (_lock)
            {
                if (_onPositionUpdate == null && _onPositionError == null)
                {
                    StopWatch();
                }
            }
        }

        private void StartWatch()
        {
            thisRef = new DotNetObjectRef(this);
            try
            {
                JSRuntime.Current.InvokeAsync<int>("rpedrettiBlazorSensors.geolocation.watchPosition", thisRef).ContinueWith(id =>
                {
                    watchId = id.Result;
                });
            }
            catch
            {
                Console.WriteLine("error getting watch id");
            }
        }

        private void StopWatch()
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorSensors.geolocation.stopWatchPosition", watchId);
            watchId = null;
            init = false;
        }

        #endregion Methods

        public event EventHandler<PositionError> OnPositionError
        {
            add
            {
                AssureInit();
                _onPositionError += value;
            }
            remove
            {
                _onPositionError -= value;
                AssureStop();
            }
        }

        public event EventHandler<Position> OnPositionUpdate
        {
            add
            {
                AssureInit();
                _onPositionUpdate += value;
            }
            remove
            {
                _onPositionUpdate -= value;
                AssureStop();
            }
        }

        [JSInvokable]
        public Task RequestPositionResponse(Position position)
        {
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task WatchPositionError(PositionError error)
        {
            _onPositionError?.Invoke(this, error);
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task WatchPositionResponse(Position position)
        {
            _onPositionUpdate?.Invoke(this, position);
            return Task.CompletedTask;
        }
    }
}
