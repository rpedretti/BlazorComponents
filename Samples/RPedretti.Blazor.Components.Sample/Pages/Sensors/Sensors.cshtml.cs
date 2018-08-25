using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Sensors.AmbientLight;
using RPedretti.Blazor.Sensors.Geolocation;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.Sample.Pages.Sensors
{
    public class SensorsBase : BaseComponent, IDisposable
    {
        #region Properties

        [Inject]
        protected GeolocationSensor GeolocationSensor { get; set; }

        [Inject]
        protected AmbientLightSensor LightSensor { get; set; }

        #endregion Properties

        #region Methods

        private void DirectionsUpdated(object sender, EventArgs e)
        {
            Console.WriteLine("directions updated");
        }

        private void OnError(object sender, string error)
        {
            LightError = error;
            StateHasChanged();
        }

        private void OnPositionError(object sender, PositionError e)
        {
            Console.WriteLine($"{e.Code.ToString()}: {e.Message}");
            StateHasChanged();
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

        public int Light { get; set; }
        public string LightError { get; set; }
        public Position Position { get; set; }
        public bool Watching { get; set; }

        public void Dispose()
        {
            LightSensor.OnReading -= OnReading;
            LightSensor.OnError -= OnError;
            GeolocationSensor.OnPositionUpdate -= OnPositionUpdate;
            GeolocationSensor.OnPositionError -= OnPositionError;
        }
    }
}
