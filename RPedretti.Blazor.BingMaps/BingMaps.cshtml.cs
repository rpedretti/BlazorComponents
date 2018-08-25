using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using RPedretti.Blazor.BingMaps.Entities;
using RPedretti.Blazor.BingMaps.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps
{
    public class BingMapsBase : BlazorComponent
    {
        #region Fields

        private ObservableCollection<IBingMapModule> _modules;
        private bool _shouldRender;
        private BingMapsViewConfig _viewConfig;
        private bool modulesLoaded;
        private DotNetObjectRef thisRef;

        #endregion Fields

        #region Methods

        private void ModulesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && modulesLoaded)
            {
                foreach (IBingMapModule item in e.NewItems)
                {
                    Console.WriteLine("init after load");
                    item.InitAsync(Id);
                }
            }
        }

        private bool SetParameter<T>(ref T prop, T value, Action onChange = null)
        {
            if (EqualityComparer<T>.Default.Equals(prop, value))
            {
                return false;
            }

            prop = value;
            onChange?.Invoke();

            return true;
        }

        private void UpdateView(BingMapsViewConfig viewConfig)
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.updateView", Id, viewConfig);
        }

        #endregion Methods

        protected bool init;
        [Parameter] protected string Id { get; set; } = $"bing-maps-{Guid.NewGuid().ToString().Replace("-", "")}";
        [Parameter] protected Func<Task> MapLoaded { get; set; }
        [Parameter] protected BingMapsConfig MapsConfig { get; set; } = new BingMapsConfig();

        [Parameter]
        protected ObservableCollection<IBingMapModule> Modules
        {
            get => _modules;
            set
            {
                if (_modules != null) _modules.CollectionChanged -= ModulesChanged;

                _modules = value;

                if (_modules != null) _modules.CollectionChanged += ModulesChanged;
            }
        }

        [Parameter]
        protected BingMapsViewConfig ViewConfig
        {
            get => _viewConfig;
            set
            {
                if (SetParameter(ref _viewConfig, value))
                {
                    UpdateView(value);
                }

                _shouldRender = true;
            }
        }

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

        [JSInvokable]
        public async Task NotifyMapLoaded()
        {
            MapLoaded?.Invoke();
            if (Modules != null)
            {
                foreach (var module in Modules)
                {
                    Console.WriteLine("init on load");
                    await module.InitAsync(Id);
                }
            }
            modulesLoaded = true;
        }
    }
}
