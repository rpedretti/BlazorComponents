using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using RPedretti.Blazor.HereMap.Extensions;

namespace RPedretti.Blazor.HereMap.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseHereMap();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
