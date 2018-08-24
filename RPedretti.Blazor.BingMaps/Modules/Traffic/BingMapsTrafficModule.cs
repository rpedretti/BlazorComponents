using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps.Modules.Traffic
{
    public class BingMapsTrafficModule : BaseBingMapModule
    {
        private const string ModuleId = "Microsoft.Maps.Traffic";
        private string _mapId;

        private string InitFunctionName =>
            "rpedrettiBlazorComponents.bingMaps.modules.traffic.init";

        public async Task UpateTrafficAsync(BingMapsTrafficOptions options)
        {
            await JSRuntime.Current.InvokeAsync<object>(
                "rpedrettiBlazorComponents.bingMaps.modules.traffic.updateTraffic",
                _mapId, options);
        }

        public override async Task InitAsync(string mapId)
        {
            _mapId = mapId;
            await InitModuleAsync(mapId, ModuleId, InitFunctionName, new { mapId, ShowTraffic = true });
        }
    }
}
