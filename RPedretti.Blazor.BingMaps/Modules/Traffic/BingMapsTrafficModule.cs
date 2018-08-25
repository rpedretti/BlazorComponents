using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps.Modules.Traffic
{
    public class BingMapsTrafficModule : BaseBingMapModule
    {
        #region Fields

        private const string ModuleId = "Microsoft.Maps.Traffic";
        private string _mapId;

        #endregion Fields

        #region Properties

        private string InitFunctionName =>
            "rpedrettiBlazorComponents.bingMaps.modules.traffic.init";

        #endregion Properties

        #region Methods

        public override async Task InitAsync(string mapId)
        {
            _mapId = mapId;
            await InitModuleAsync(mapId, ModuleId, InitFunctionName, new { mapId, ShowTraffic = true });
        }

        public async Task UpateTrafficAsync(BingMapsTrafficOptions options)
        {
            await JSRuntime.Current.InvokeAsync<object>(
                "rpedrettiBlazorComponents.bingMaps.modules.traffic.updateTraffic",
                _mapId, options);
        }

        #endregion Methods
    }
}
