using BlazorApp40.Services;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorApp40
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddSingleton<IMovieService, ImdbService>();
                services.AddSingleton<IForecastService, ForecastService>();
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
