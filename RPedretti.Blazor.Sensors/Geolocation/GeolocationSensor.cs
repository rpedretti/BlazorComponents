using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Sensors.Geolocation
{
    public class GeolocationSensor
    {
        #region Fields

        private DotNetObjectRef thisRef;

        private int? watchId;
        private bool init;

        #endregion Fields

        #region Events

        private event EventHandler<Position> _onPositionUpdate;
        private event EventHandler<PositionError> _onPositionError;

        #endregion Events

        #region Methods

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
            Console.WriteLine($"stop id: {watchId}");
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorSensors.geolocation.stopWatchPosition", watchId);
            watchId = null;
            init = false;
        }

        #endregion Methods

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

        [JSInvokable]
        public Task RequestPositionResponse(Position position)
        {
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task WatchPositionResponse(Position position)
        {
            _onPositionUpdate?.Invoke(this, position);
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task WatchPositionError(PositionError error)
        {
            _onPositionError?.Invoke(this, error);
            return Task.CompletedTask;
        }

        private object _lock = new Object();
    }
}
