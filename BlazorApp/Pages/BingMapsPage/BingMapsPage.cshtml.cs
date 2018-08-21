using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components;
using RPedretti.Blazor.Components.BingMaps.Entities;
using RPedretti.Blazor.Components.BingMaps.Modules;
using RPedretti.Blazor.Components.BingMaps.Modules.Directions;
using RPedretti.Blazor.Components.BingMaps.Modules.Traffic;
using RPedretti.Blazor.Components.BingMaps.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages.BingMapsPage
{
    public class BingMapsPageBase: BaseComponent
    {
        [Inject] protected BingMapPushpinService BingMapPushpinService { get; set; }

        protected string BingMapId = $"bing-maps-{Guid.NewGuid().ToString().Replace("-", "")}";

        protected ObservableCollection<IBingMapModule> Modules;

        private bool _showIncidents = true;
        protected bool ShowIncidents
        {
            get => _showIncidents;
            set
            {
                if (SetParameter(ref _showIncidents, value))
                {
                    UpdateTraffic(true);
                }

            }
        }
        protected bool ShowTraffic { get; set; }

        private BingMapsTrafficModule _trafficModule;
        private BingMapsDirectionsModule _directionsModule;

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

        public BingMapsPageBase()
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

        protected async void UpdateTraffic(bool show)
        {
            ShowTraffic = show;
            if (!Modules.Any(m => m is BingMapsTrafficModule))
            {
                _trafficModule = new BingMapsTrafficModule();
                Modules.Add(_trafficModule);
            }
            else
            {
                await _trafficModule.UpateTrafficAsync(new BingMapsTrafficOptions
                {
                    FlowVisible = ShowTraffic,
                    IncidentsVisible = ShowTraffic && ShowIncidents,
                    LegendVisible = ShowTraffic
                });
            }

            StateHasChanged();
        }

        public Task MapLoaded()
        {
            return Task.CompletedTask;
        }

        protected BingMapsViewConfig MapsViewConfig { get; set; } = new BingMapsViewConfig();
    }
}
