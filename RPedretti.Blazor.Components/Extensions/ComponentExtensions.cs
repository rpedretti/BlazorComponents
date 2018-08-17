using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.JSInterop;

namespace RPedretti.Blazor.Components.Extensions
{
    public static class ComponentExtensions
    {
        public static IBlazorApplicationBuilder UseBingMaps(this IBlazorApplicationBuilder applicationBuilder, string apiKey)
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.initScript", apiKey);
            return applicationBuilder;
        }
    }
}
