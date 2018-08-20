using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using RPedretti.Blazor.Components.BingMaps.Entities;
using RPedretti.Blazor.Components.BingMaps.Modules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.BingMaps
{
    public class BingMapsBase : BaseComponent
    {
        protected const string scriptUrl = "https://www.bing.com/api/maps/mapcontrol?callback=GetMap&key=";
        protected bool init;
        private bool _shouldRender;
        private DotNetObjectRef thisRef;

        [Parameter] protected string ApiKey { get; set; }
        [Parameter] protected IEnumerable<IBingMapModule> Modules { get; set; } = new IBingMapModule[0];

        [Parameter] protected BingMapsConfig MapsConfig { get; set; } = new BingMapsConfig();

        private BingMapsViewConfig _viewConfig;
        [Parameter] protected BingMapsViewConfig ViewConfig {
            get => _viewConfig;
            set {
                if (SetParameter(ref _viewConfig, value))
                {
                    UpdateView(value);
                }

                _shouldRender = true;
            }
        }

        [Parameter] protected string Id { get; set; } = $"bing-maps-{Guid.NewGuid().ToString().Replace("-", "")}";

        protected override void OnAfterRender()
        {
            if (!init)
            {
                init = true;
                thisRef = new DotNetObjectRef(this);
                JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.getMap", thisRef, Id, MapsConfig);
            }

            _shouldRender = false;
        }

        protected override bool ShouldRender()
        {
            return _shouldRender;
        }

        private void UpdateView(BingMapsViewConfig viewConfig)
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.updateView", Id, viewConfig);
        }

        [JSInvokable]
        public async Task MapLoaded()
        {
            foreach (var module in Modules)
            {
                await module.InitAsync(Id);
            }
        }
    }
}
