using BlazorApp30.ViewModel;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorApp30
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddTransient<TodoPageViewModel>();
                services.AddTransient<CounterPageViewModel>();
                services.AddTransient<FetchDataPageViewModel>();
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
