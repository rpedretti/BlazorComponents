using BlazorApp.Managers;
using BlazorApp.Services;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using RPedretti.Blazor.Sensors.Extensions;

namespace BlazorApp
{
    public class Startup
    {
        #region Methods

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
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
        }

        #endregion Methods
    }
}
