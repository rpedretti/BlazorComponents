using Microsoft.AspNetCore.Blazor.Hosting;

namespace RPedretti.Blazor.BingMap.Sample
{
    public class Program
    {
        #region Methods

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        #endregion Methods
    }
}
