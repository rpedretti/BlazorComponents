using Blazor.Extensions.Logging;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RPedretti.Blazor.BingMap.Extensions;

namespace RPedretti.Blazor.BingMap.Sample
{
    public class Startup
    {
        #region Methods

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            app.UseBingMaps("AkUyQ5km3V0tUHk_BL1gRFWunbT1x6dlbH_0mUHDREAHPgsJ1LlpS0ma2-0SIvV7");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddBrowserConsole() // Add Blazor.Extensions.Logging.BrowserConsoleLogger
                .SetMinimumLevel(LogLevel.Trace)
            );
        }

        #endregion Methods
    }
}
