using Microsoft.Extensions.DependencyInjection;

namespace RPedretti.Blazor.BingMaps.Services
{
    public static class BingMapServiceExtensions
    {
        #region Methods

        public static IServiceCollection AddBingMapPushpinService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<BingMapPushpinService>();
            return serviceCollection;
        }

        #endregion Methods
    }
}
