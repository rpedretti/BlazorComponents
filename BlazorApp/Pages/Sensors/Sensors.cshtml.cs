using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components;
using RPedretti.Blazor.Components.BingMaps;
using RPedretti.Blazor.Components.BingMaps.Modules;
using RPedretti.Blazor.Components.BingMaps.Modules.Directions;
using RPedretti.Blazor.Sensors.AmbientLight;
using RPedretti.Blazor.Sensors.Geolocation;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Sensors
{
    public class SensorsBase : BaseComponent, IDisposable
    {
        #region Properties

        [Inject] protected GeolocationSensor GeolocationSensor { get; set; }
        [Inject] protected AmbientLightSensor LightSensor { get; set; }
        public int Light { get; set; }
        public string LightError { get; set; }
        public Position Position { get; set; }
        public bool Watching { get; set; }

        protected IBingMapModule[] Modules = new IBingMapModule[] {
            new BingMapsDirectionsModule {
                InputPanelId = "inputPannel",
                ItineraryPanelId = "itineraryPanel"
            }
        };

        protected BingMapsViewConfig MapsViewConfig { get; set; } = new BingMapsViewConfig
        {
            MapTypeId = BingMapsTypes.GrayScale,
            Zoom = 12
        };

        #endregion Properties

        #region Methods

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
            MapsViewConfig = new BingMapsViewConfig
            {
                Center = new Geocoordinate { Latitude = e.Coords.Latitude, Longitude = e.Coords.Longitude, Altitude = e.Coords.Altitude ?? 0},
                Zoom = 15
            };
            StateHasChanged();
        }

        private void OnReading(object sender, int reading)
        {
            Light = reading;
            MapsViewConfig = new BingMapsViewConfig
            {
                MapTypeId = reading > 100 ? BingMapsTypes.CanvasLight : BingMapsTypes.CanvasDark
            };

            StateHasChanged();
        }

        protected override void OnInit()
        {
            LightSensor.OnReading += OnReading;
            LightSensor.OnError += OnError;
        }

        protected Task StartWatch()
        {
            MapsViewConfig = null;
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

        #endregion Methods
    }
}
