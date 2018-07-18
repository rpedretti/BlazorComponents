using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RPedretti.Blazor.SignalRServer.Hubs;

namespace RPedretti.Blazor.SignalRServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(
                        "http://192.168.15.13:1234",
                        "http://192.168.15.10:1234",
                        "http://localhost:1234",
                        "http://blazorapp40.azurewebsites.net"
                    )
                    .AllowCredentials();
            });

            app.UseSignalR(route =>
            {
                route.MapHub<BlazorHub>("/blazorhub");
            });

            app.UseMvc();
        }
    }
}
