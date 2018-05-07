using BlazorApp.Services;
using BlazorApp.ViewModel;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddSingleton<IBaseService, BaseService>();
                services.AddTransient<TodoPageViewModel>();
                services.AddTransient<CounterPageViewModel>();
                services.AddTransient<FetchDataPageViewModel>();
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
