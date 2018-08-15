using Microsoft.AspNetCore.Blazor.Components;
using Newtonsoft.Json;
using RPedretti.Blazor.Components;
using RPedretti.Blazor.Sensors.AmbientLight;
using RPedretti.Blazor.Sensors.Geolocation;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Sensors
{
    public class SensorsBase : BaseComponent, IDisposable
    {
        #region Methods

        private void OnError(object sender, object e)
        {
            Console.WriteLine($"On error .NET: {e}");
        }

        private void OnPositionError(object sender, PositionError e)
        {
            Console.WriteLine($"{e.Code.ToString()}: {e.Message}");
        }

        private void OnPositionUpdate(object sender, Position e)
        {
            Position = e;
            StateHasChanged();
        }

        private void OnReading(object sender, int reading)
        {
            Light = reading;
            StateHasChanged();
        }

        #endregion Methods

        #region Properties

        [Inject] protected GeolocationSensor GeolocationSensor { get; set; }
        [Inject] protected AmbientLightSensor LightSensor { get; set; }

        public int Light { get; set; }
        public Position Position { get; set; }
        public bool Watching { get; set; }

        #endregion Properties

        protected override void OnInit()
        {
            LightSensor.OnReading += OnReading;
            LightSensor.OnError += OnError;
        }

        protected Task StartWatch()
        {
            GeolocationSensor.OnPositionUpdate += OnPositionUpdate;
            GeolocationSensor.OnPositionError += OnPositionError;
            Watching = true;
            return Task.CompletedTask;
        }

        protected Task StopWatch()
        {
            GeolocationSensor.OnPositionUpdate -= OnPositionUpdate;
            GeolocationSensor.OnPositionError -= OnPositionError;
            Position = null;
            Watching = false;

            return Task.CompletedTask;
        }

        public new void Dispose()
        {
            LightSensor.OnReading -= OnReading;
            LightSensor.OnError -= OnError;
            GeolocationSensor.OnPositionUpdate -= OnPositionUpdate;
            GeolocationSensor.OnPositionError -= OnPositionError;
            base.Dispose();
        }
    }
}
