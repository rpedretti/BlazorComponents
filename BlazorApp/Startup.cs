using BlazorApp.Managers;
using BlazorApp.Services;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using RPedretti.Blazor.Sensors.AmbientLight;
using RPedretti.Blazor.Sensors.Extensions;
using System;

namespace BlazorApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMovieService, ImdbService>();
            services.AddSingleton<IForecastService, ForecastService>();

            services.AddSingleton<NotificationManager>();
            services.AddSingleton<DownloadManager>();
            services.AddSingleton<BlazorHubConnectionManager>();
            services.AddStorage();
            services.AddAmbientLightSensor();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            app.InitAmbientLightSensor();
        }
    }
}
