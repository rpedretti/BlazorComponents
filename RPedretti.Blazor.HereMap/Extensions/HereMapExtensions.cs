using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using RPedretti.Blazor.HereMap.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.HereMap.Extensions
{
    public static class HereMapExtensions
    {
        public static IServiceCollection UseHereMap(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<HereMapService>();
            return serviceCollection;
        }

        public static async Task AddDefaultBehaviour(this HereMap map)
        {
            await JSRuntime.Current.InvokeAsync<int>($"{Constants.MapNamespace}.addDefaultBehaviour", map.Id);
        }

        public static async Task AddDefaultUi(this HereMap map)
        {
            await JSRuntime.Current.InvokeAsync<int>($"{Constants.MapNamespace}.addDefaultUi", map.Id);
        }
    }
}
