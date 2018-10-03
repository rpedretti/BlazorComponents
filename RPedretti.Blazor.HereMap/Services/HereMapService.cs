using Microsoft.JSInterop;
using RPedretti.Blazor.HereMap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.HereMap.Services
{
    public class HereMapService
    {
        public async Task InitService(string appId, string appCode)
        {
            await JSRuntime.Current.InvokeAsync<int>(
                $"{Constants.MapNamespace}.initService",
                appId,
                appCode
            );
        }

        public async Task LoadModules(IEnumerable<HereMapModule> modules)
        {
            await JSRuntime.Current.InvokeAsync<int>($"{Constants.MapNamespace}.loadModules",
                modules.Select(m => m.ToString()));
        }
    }
}
