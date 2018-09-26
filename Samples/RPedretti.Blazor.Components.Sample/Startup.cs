using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using RPedretti.Blazor.Components.Sample.Managers;
using RPedretti.Blazor.Components.Sample.Services;
using RPedretti.Blazor.Sensors.Extensions;
using Microsoft.Extensions.Logging;
using Blazor.Extensions.Logging;

namespace RPedretti.Blazor.Components.Sample
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
            services.AddLogging(builder => builder
                .AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Trace)
            );
        }

        #endregion Methods
    }
}
