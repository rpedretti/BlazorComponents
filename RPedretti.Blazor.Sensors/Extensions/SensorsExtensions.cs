using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using RPedretti.Blazor.Sensors.AmbientLight;
using RPedretti.Blazor.Sensors.Geolocation;

namespace RPedretti.Blazor.Sensors.Extensions
{
    public static class SensorsExtensions
    {
        #region Methods

        public static IServiceCollection AddAmbientLightSensor(this IServiceCollection services)
        {
            services.AddSingleton<AmbientLightSensor>();
            return services;
        }

        public static IServiceCollection AddGeolocationSensor(this IServiceCollection services)
        {
            services.AddSingleton<GeolocationSensor>();
            return services;
        }

        public static void InitAmbientLightSensor(this IBlazorApplicationBuilder app)
        {
            var sensor = app.Services.GetService<AmbientLightSensor>();
            sensor.Init();
        }

        #endregion Methods
    }
}
