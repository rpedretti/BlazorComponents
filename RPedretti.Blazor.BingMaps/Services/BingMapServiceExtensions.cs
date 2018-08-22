using Microsoft.Extensions.DependencyInjection;

namespace RPedretti.Blazor.BingMaps.Services
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
