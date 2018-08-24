using RPedretti.Blazor.BingMaps.Entities;
using RPedretti.Blazor.BingMaps.Modules;
using RPedretti.Blazor.BingMaps.Modules.Directions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps.Sample.Pages.Directions
{
    public class DirectionsBase : BaseComponent
    {
        private BingMapsDirectionsModule _directionsModule;
        protected ObservableCollection<IBingMapModule> Modules;
        protected string BingMapId = $"bing-maps-{Guid.NewGuid().ToString().Replace("-", "")}";


        public Task MapLoaded()
        {
            return Task.CompletedTask;
        }

        protected BingMapsViewConfig MapsViewConfig { get; set; } = new BingMapsViewConfig();

        protected BingMapsConfig MapsConfig { get; set; } = new BingMapsConfig
        {
            MapTypeId = BingMapsTypes.GrayScale,
            SupportedMapTypes = new string[] {
                BingMapsTypes.Aerial,
                BingMapsTypes.GrayScale,
                BingMapsTypes.Road,
                BingMapsTypes.BirdsEyes
            },
            EnableHighDpi = true,
            Zoom = 12,
            ShowTrafficButton = true

        };

        public DirectionsBase()
        {
            _directionsModule = new BingMapsDirectionsModule
            {
                InputPanelId = "inputPannel",
                ItineraryPanelId = "itineraryPanel"
            };

            _directionsModule.DirectionsUpdated += DirectionsUpdated;
            Modules = new ObservableCollection<IBingMapModule> { _directionsModule };
        }

        private void DirectionsUpdated(object sender, EventArgs e)
        {
            Console.WriteLine("path updated");
        }
    }
}
