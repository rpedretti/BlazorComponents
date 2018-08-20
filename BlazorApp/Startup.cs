using BlazorApp.Managers;
using BlazorApp.Services;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using RPedretti.Blazor.Components.BingMaps.Services;
using RPedretti.Blazor.Components.Extensions;
using RPedretti.Blazor.Sensors.Extensions;

namespace BlazorApp
{
    public class Startup
    {
        #region Methods

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            app.UseBingMaps("AkUyQ5km3V0tUHk_BL1gRFWunbT1x6dlbH_0mUHDREAHPgsJ1LlpS0ma2-0SIvV7");
            app.InitAmbientLightSensor();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMovieService, ImdbService>();
            services.AddSingleton<IForecastService, ForecastService>();

            services.AddSingleton<NotificationManager>();
            services.AddSingleton<DownloadManager>();
            services.AddSingleton<BlazorHubConnectionManager>();
            services.AddStorage();
            services.AddAmbientLightSensor();
            services.AddGeolocationSensor();
            services.AddBingMapPushpinService();
        }

        #endregion Methods
    }
}
