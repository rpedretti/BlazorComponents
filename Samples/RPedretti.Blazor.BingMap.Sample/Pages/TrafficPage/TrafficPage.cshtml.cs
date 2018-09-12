using RPedretti.Blazor.BingMap.Entities;
using RPedretti.Blazor.BingMap.Modules;
using RPedretti.Blazor.BingMap.Modules.Traffic;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RPedretti.Blazor.BingMap.Sample.Pages.TrafficPage
{
    public class TrafficPageBase : BaseComponent
    {
        #region Fields

        private bool _showIncidents = true;
        private BingMapTrafficModule _trafficModule;
        protected string BingMapId = $"bing-map-{Guid.NewGuid().ToString().Replace("-", "")}";
        protected ObservableCollection<IBingMapModule> Modules = new ObservableCollection<IBingMapModule>();

        #endregion Fields

        #region Properties

        protected BingMapConfig MapsConfig { get; set; } = new BingMapConfig
        {
            MapTypeId = BingMapTypes.Road,
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

        #endregion Properties

        #region Methods

        protected async void UpdateTraffic(bool show)
        {
            ShowTraffic = show;
            if (!Modules.Any(m => m is BingMapTrafficModule))
            {
                _trafficModule = new BingMapTrafficModule();
                Modules.Add(_trafficModule);
            }
            else
            {
                await _trafficModule.UpateTrafficAsync(new BingMapTrafficOptions
                {
                    FlowVisible = ShowTraffic,
                    IncidentsVisible = ShowTraffic && ShowIncidents,
                    LegendVisible = ShowTraffic
                });
            }

            StateHasChanged();
        }

        #endregion Methods
    }
}
