using Blazor.Extensions;
using Blazor.Fluxor;
using BlazorApp.Services;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Program : IDisposable
    {
        private static HubConnection connection;

        #region Methods

        private static void Main(string[] args)
        {
            connection = new HubConnectionBuilder()
#if DEBUG
            .WithUrl("http://localhost:5000/blazorhub", opt =>
#else
            .WithUrl("http://blazorsignalr.azurewebsites.net/blazorhub", opt =>
#endif
            {
                opt.SkipNegotiation = true;
                opt.LogLevel = SignalRLogLevel.Debug;
                opt.Transport = HttpTransportType.WebSockets;
            })
            .Build();

            connection.StartAsync();

            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddSingleton<IMovieService, ImdbService>();
                services.AddSingleton<IForecastService, ForecastService>();
                services.AddFluxor(options => options
                    .UseDependencyInjection(typeof(Program).Assembly)
                    .AddMiddleware<Blazor.Fluxor.ReduxDevTools.ReduxDevToolsMiddleware>()
                    .AddMiddleware<Blazor.Fluxor.Routing.RoutingMiddleware>()
                );
                services.AddSingleton(connection);
                services.AddSingleton<DownloadManager>();
                services.AddStorage();
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }

        public async void Dispose()
        {
            await connection.StopAsync();
            connection.Dispose();
        }

#endregion Methods
    }
}
