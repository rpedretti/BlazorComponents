using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.BingMaps.Modules.Directions
{
    public class BingMapsDirectionsModule : BaseBingMapModule, IDisposable
    {
        private DotNetObjectRef thisRef;
        private const string ModuleId = "Microsoft.Maps.Directions";
        private string InitFunctionName =>
            "rpedrettiBlazorComponents.bingMaps.modules.directions.init";

        public event EventHandler DirectionsUpdated;

        public string InputPanelId { get; set; }
        public string ItineraryPanelId { get; set; }
        
        public override async Task InitAsync(string mapId)
        {
            thisRef = new DotNetObjectRef(this);
            var param = new { InputPanelId, ItineraryPanelId, ModuleRef = thisRef };
            await InitModuleAsync(mapId, ModuleId, InitFunctionName, param);
        }

        [JSInvokable]
        public Task DirectionsUpdatedAsync()
        {
            DirectionsUpdated?.Invoke(this, EventArgs.Empty);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            thisRef.Dispose();
        }
    }
}
