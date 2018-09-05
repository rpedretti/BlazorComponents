using RPedretti.Blazor.BingMap.Entities;
using RPedretti.Blazor.BingMap.Modules;
using RPedretti.Blazor.BingMap.Modules.Directions;
using System;
using System.Collections.ObjectModel;

namespace RPedretti.Blazor.BingMap.Sample.Pages.DirectionsPage
{
    public class DirectionsPageBase : BaseComponent
    {
        #region Fields

        private BingMapDirectionsModule _directionsModule;

        #endregion Fields

        #region Methods

        private void DirectionsUpdated(object sender, EventArgs e)
        {
            Console.WriteLine("path updated");
        }

        #endregion Methods

        protected string BingMapId = $"bing-map-{Guid.NewGuid().ToString().Replace("-", "")}";
        protected ObservableCollection<IBingMapModule> Modules;

        protected BingMapConfig MapsConfig { get; set; } = new BingMapConfig
        {
            MapTypeId = BingMapTypes.GrayScale,
            SupportedMapTypes = new string[] {
                BingMapTypes.Aerial,
                BingMapTypes.GrayScale,
                BingMapTypes.Road,
                BingMapTypes.BirdsEyes
            },
            EnableHighDpi = true,
            Zoom = 12,
            ShowTrafficButton = true
        };

        protected BingMapsViewConfig MapsViewConfig { get; set; } = new BingMapsViewConfig();

        public DirectionsPageBase()
        {
            _directionsModule = new BingMapDirectionsModule
            {
                InputPanelId = "inputPannel",
                ItineraryPanelId = "itineraryPanel"
            };

            _directionsModule.DirectionsUpdated += DirectionsUpdated;
            Modules = new ObservableCollection<IBingMapModule> { _directionsModule };
        }
    }
}
