using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.JSInterop;

namespace RPedretti.Blazor.BingMaps.Extensions
{
    public static class ComponentExtensions
    {
        public static IBlazorApplicationBuilder UseBingMaps(
            this IBlazorApplicationBuilder applicationBuilder,
            string apiKey,
            string mapLanguage = null)
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.initScript", apiKey, mapLanguage);
            return applicationBuilder;
        }
    }
}
