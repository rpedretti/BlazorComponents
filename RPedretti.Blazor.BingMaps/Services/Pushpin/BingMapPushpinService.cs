using RPedretti.Blazor.BingMaps.Entities;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps.Services
{
    public class BingMapPushpinService
    {
        public Dictionary<string, HashSet<string>> pushpins { get; set; } = new Dictionary<string, HashSet<string>>();

        public Task AddPushpin(string mapId, BingMapPushpin pushpin)
        {
            if (!pushpins.ContainsKey(mapId))
            {
                pushpins[mapId] = new HashSet<string>();
            }
            pushpins[mapId].Add(pushpin.Id);

            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.pushpin.add", mapId, pushpin);
            return Task.CompletedTask;
        }

        public bool ContainsPushpin(string mapId, string pushpinId)
        {
            return pushpins.ContainsKey(mapId) && pushpins[mapId].Contains(pushpinId);
        }

        public Task UpdatePushpinLocation(string mapId, string pushpinId, Geocoordinate location)
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.pushpin.updateLocation", mapId, pushpinId, location);
            return Task.CompletedTask;
        }

        public Task UpdatePushpinOptions(string mapId, string pushpinId, PushpinOptions pushpinOptions)
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.pushpin.updateOptions", mapId, pushpinId, pushpinOptions);
            return Task.CompletedTask;
        }

        public Task DeletePushpin(string mapId, string pushpinId)
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.pushpin.remove", mapId, pushpinId);
            pushpins[mapId].Remove(pushpinId);
            return Task.CompletedTask;
        }
    }
}
