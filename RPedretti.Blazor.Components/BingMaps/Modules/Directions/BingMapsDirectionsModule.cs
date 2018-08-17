using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.BingMaps.Modules.Directions
{
    public class BingMapsDirectionsModule : IBingMapModule
    {
        public static string ModuleId = "Microsoft.Maps.Directions";
        public string InputPanelId { get; set; }
        public string ItineraryPanelId { get; set; }
        public string InitFunctionName { get; set; } =
            "window.rpedrettiBlazorComponents.bingMaps.modules.directions.init";

        public async Task InitAsync(string mapId)
        {
            await JSRuntime.Current.InvokeAsync<object>(
                "window.rpedrettiBlazorComponents.bingMaps.loadModule",
                mapId,
                ModuleId,
                InitFunctionName,
                new { InputPanelId, ItineraryPanelId });
        }
    }
}
