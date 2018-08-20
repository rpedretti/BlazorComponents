using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.BingMaps.Services
{
    public static class BingMapServiceExtensions
    {
        public static IServiceCollection AddBingMapPushpinService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<BingMapPushpinService>();
            return serviceCollection;
        }
    }
}
