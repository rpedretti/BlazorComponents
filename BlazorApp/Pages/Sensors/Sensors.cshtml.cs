using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components;
using RPedretti.Blazor.Components.BingMaps;
using RPedretti.Blazor.Components.BingMaps.Entities;
using RPedretti.Blazor.Components.BingMaps.Modules;
using RPedretti.Blazor.Components.BingMaps.Modules.Directions;
using RPedretti.Blazor.Components.BingMaps.Services;
using RPedretti.Blazor.Sensors.AmbientLight;
using RPedretti.Blazor.Sensors.Geolocation;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Sensors
{
    public class SensorsBase : BaseComponent, IDisposable
    {
        #region Properties

        [Inject] protected GeolocationSensor GeolocationSensor { get; set; }
        [Inject] protected AmbientLightSensor LightSensor { get; set; }
        [Inject] protected BingMapPushpinService BingMapPushpinService { get; set; }

        public int Light { get; set; }
        public string LightError { get; set; }
        public Position Position { get; set; }
        public bool Watching { get; set; }

        protected string BingMapId = $"bing-maps-{Guid.NewGuid().ToString().Replace("-", "")}";

        protected IBingMapModule[] Modules = new IBingMapModule[] {
            new BingMapsDirectionsModule {
                InputPanelId = "inputPannel",
                ItineraryPanelId = "itineraryPanel"
            }
        };

        protected BingMapsConfig MapsConfig { get; set; } = new BingMapsConfig
        {
            MapTypeId = BingMapsTypes.GrayScale,
            SupportedMapTypes = new string[] {
                BingMapsTypes.Aerial,
                BingMapsTypes.GrayScale,
                BingMapsTypes.Road,
                BingMapsTypes.BirdsEyes
            },
            Zoom = 12,
            NavigationBarOrientation = NavigationBarOrientation.Hotizontal,
            NavigationBarMode = NavigationBarMode.Compact,

        };

        protected BingMapsViewConfig MapsViewConfig { get; set; } = new BingMapsViewConfig();

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
            var center = new Geocoordinate { Latitude = e.Coords.Latitude, Longitude = e.Coords.Longitude, Altitude = e.Coords.Altitude ?? 0 };
            
            if (BingMapPushpinService.ContainsPushpin(BingMapId, "me"))
            {
                BingMapPushpinService.UpdatePushpinLocation(BingMapId, "me", center);
            }
            else
            {
                MapsViewConfig = new BingMapsViewConfig
                {
                    Center = center,
                    Zoom = 15
                };

                BingMapPushpinService.AddPushpin(BingMapId, new BingMapPushpin()
                {
                    Id = "me",
                    Center = center,
                    Options = new PushpinOptions()
                    {
                        Titlte = "Me",
                        Color = Color.Red
                    }
                });

                StateHasChanged();
            }

        }

        private void OnReading(object sender, int reading)
        {
            if (reading >= 200 && Light < 200 || reading < 200 && Light >= 200)
            {
                MapsViewConfig = new BingMapsViewConfig
                {
                    MapTypeId = reading > 200 ? BingMapsTypes.CanvasLight : BingMapsTypes.CanvasDark
                };
            }

            Light = reading;
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
            BingMapPushpinService.DeletePushpin(BingMapId, "me");
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
