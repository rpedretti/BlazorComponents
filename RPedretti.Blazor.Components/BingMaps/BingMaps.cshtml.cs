using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.BingMaps
{
    public class BingMapsBase : BaseComponent
    {
        protected const string scriptUrl = "https://www.bing.com/api/maps/mapcontrol?callback=GetMap&key=";
        protected bool init;
        private bool _shouldRender;

        [Parameter] protected string ApiKey { get; set; }

        private BingMapsViewConfig _config;
        [Parameter] protected BingMapsViewConfig Config {
            get => _config;
            set {
                if (SetParameter(ref _config, value))
                {
                    UpdateView(value);
                }

                _shouldRender = true;
            }
        }

        protected string Id { get; set; }

        public BingMapsBase()
        {
            Id = $"bing-maps-{Guid.NewGuid().ToString().Replace("-", "")}";
        }

        protected override void OnAfterRender()
        {
            if (!init)
            {
                init = true;
                var initialConfig = new { Credentials = ApiKey, Config.MapTypeId, Config.Zoom };
                JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.getMap", Id, initialConfig);
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
    }
}
