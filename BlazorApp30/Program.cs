using BlazorApp30.Services;
using BlazorApp30.ViewModel;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp30
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddSingleton<IMovieService, ImdbService>();
                services.AddTransient<TodoPageViewModel>();
                services.AddTransient<CounterPageViewModel>();
                services.AddTransient<FetchDataPageViewModel>();
                services.AddTransient<IndexPageViewModel>();
                services.AddTransient<MoviesPageViewModel>();
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
