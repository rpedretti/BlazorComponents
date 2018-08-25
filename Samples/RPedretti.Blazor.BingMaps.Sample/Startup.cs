using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using RPedretti.Blazor.BingMaps.Extensions;
using RPedretti.Blazor.BingMaps.Services;

namespace RPedretti.Blazor.BingMaps.Sample
{
    public class Startup
    {
        #region Methods

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            app.UseBingMaps("<your_bing_maps_api_key>");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBingMapPushpinService();
        }

        #endregion Methods
    }
}
