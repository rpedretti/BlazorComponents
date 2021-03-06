﻿using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.JSInterop;

namespace RPedretti.Blazor.BingMap.Extensions
{
    public static class ComponentExtensions
    {
        #region Methods

        public static IBlazorApplicationBuilder UseBingMaps(
            this IBlazorApplicationBuilder applicationBuilder,
            string apiKey,
            string mapLanguage = null)
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.map.initScript", apiKey, mapLanguage);
            return applicationBuilder;
        }

        #endregion Methods
    }
}
