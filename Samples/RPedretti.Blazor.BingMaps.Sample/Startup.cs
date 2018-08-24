using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using RPedretti.Blazor.BingMaps.Extensions;
using RPedretti.Blazor.BingMaps.Services;

namespace RPedretti.Blazor.BingMaps.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBingMapPushpinService();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            app.UseBingMaps("AkUyQ5km3V0tUHk_BL1gRFWunbT1x6dlbH_0mUHDREAHPgsJ1LlpS0ma2-0SIvV7");
        }
    }
}
