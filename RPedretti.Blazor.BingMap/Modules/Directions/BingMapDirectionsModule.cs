using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Modules.Directions
{
    public class BingMapDirectionsModule : BaseBingMapModule, IDisposable
    {
        #region Fields

        private const string ModuleId = "Microsoft.Maps.Directions";
        private DotNetObjectRef thisRef;

        #endregion Fields

        #region Properties

        private string InitFunctionName =>
            "rpedrettiBlazorComponents.bingMaps.map.modules.directions.init";

        public string InputPanelId { get; set; }

        public string ItineraryPanelId { get; set; }

        #endregion Properties

        #region Events

        public event EventHandler DirectionsUpdated;

        #endregion Events

        #region Methods

        [JSInvokable]
        public Task DirectionsUpdatedAsync()
        {
            DirectionsUpdated?.Invoke(this, EventArgs.Empty);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            JSRuntime.Current.UntrackObjectRef(thisRef);
        }

        public override async Task InitAsync(string mapId)
        {
            thisRef = new DotNetObjectRef(this);
            var param = new { InputPanelId, ItineraryPanelId, ModuleRef = thisRef };
            await InitModuleAsync(mapId, ModuleId, InitFunctionName, param);
        }

        #endregion Methods
    }
}
