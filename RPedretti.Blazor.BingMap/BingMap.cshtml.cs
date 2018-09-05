using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using RPedretti.Blazor.BingMap.Entities;
using RPedretti.Blazor.BingMap.Entities.Layer;
using RPedretti.Blazor.BingMap.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap
{
    public class BingMapBase : BlazorComponent, IDisposable
    {
        #region Fields

        private Shared.Collections.BindingList<BaseBingMapEntity> _entities;
        private Shared.Collections.BindingList<BingMapLayer> _layers;
        private ObservableCollection<IBingMapModule> _modules;
        private bool _shouldRender;
        private BingMapsViewConfig _viewConfig;
        private bool modulesLoaded;
        private DotNetObjectRef thisRef;

        #endregion Fields

        #region Methods

        private async Task AddItem(BaseBingMapEntity baseBingMapEntity)
        {
            try
            {
                await JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.addItem", Id, baseBingMapEntity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void EntitiesChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    AddItem(_entities[e.NewIndex]);
                    break;

                case ListChangedType.ItemChanged:
                    UpdateItem(_entities[e.NewIndex]);
                    break;
            }
        }

        private async void EntitiesRemoved(object sender, BaseBingMapEntity removed)
        {
            try
            {
                await JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.removeItem", Id, removed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void LayerRemoved(object sender, BingMapLayer removed)
        {
            try
            {
                await JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.removeLayer", Id, removed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void LayersChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.ItemAdded:
                        await JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.addLayer", Id, _layers[e.NewIndex].Id);
                        break;
                    case ListChangedType.ItemChanged:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ModulesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && modulesLoaded)
            {
                foreach (IBingMapModule item in e.NewItems)
                {
                    item.InitAsync(Id);
                }
            }
        }

        private async Task RemoveItem(BaseBingMapEntity baseBingMapEntity)
        {
            try
            {
                await JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.removeItem", Id, baseBingMapEntity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        private async Task UpdateItem(BaseBingMapEntity baseBingMapEntity)
        {
            try
            {
                await JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.updateItem", Id, baseBingMapEntity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateView(BingMapsViewConfig viewConfig)
        {
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.updateView", Id, viewConfig);
        }

        #endregion Methods

        protected bool init;

        [Parameter]
        protected Shared.Collections.BindingList<BaseBingMapEntity> Entities
        {
            get => _entities;
            set
            {
                if (!EqualityComparer<BindingList<BaseBingMapEntity>>.Default.Equals(_entities, value))
                {
                    if (_entities != null)
                    {
                        _entities.ListChanged -= EntitiesChanged;
                        _entities.BeforeRemove -= EntitiesRemoved;
                    }

                    _entities = value;

                    if (_entities != null)
                    {
                        _entities.ListChanged += EntitiesChanged;
                        _entities.BeforeRemove += EntitiesRemoved;
                    }
                }
            }
        }

        [Parameter] protected string Id { get; set; } = $"bing-map-{Guid.NewGuid().ToString().Replace("-", "")}";

        [Parameter]
        protected Shared.Collections.BindingList<BingMapLayer> Layers
        {
            get => _layers;
            set
            {
                if (_layers != null)
                {
                    _layers.ListChanged -= LayersChanged;
                    _layers.BeforeRemove -= LayerRemoved;
                }

                _layers = value;

                if (_layers != null)
                {
                    _layers.ListChanged += LayersChanged;
                    _layers.BeforeRemove += LayerRemoved;
                }
            }
        }

        [Parameter] protected Func<Task> MapLoaded { get; set; }
        [Parameter] protected BingMapConfig MapsConfig { get; set; } = new BingMapConfig();

        [Parameter]
        protected ObservableCollection<IBingMapModule> Modules
        {
            get => _modules;
            set
            {
                if (_modules != null)
                {
                    _modules.CollectionChanged -= ModulesChanged;
                }

                _modules = value;

                if (_modules != null)
                {
                    _modules.CollectionChanged += ModulesChanged;
                }
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

        public void Dispose()
        {
            Modules = null;
            Entities = null;
            Layers = null;
            MapLoaded = null;

            JSRuntime.Current.UntrackObjectRef(thisRef);
            JSRuntime.Current.InvokeAsync<object>("rpedrettiBlazorComponents.bingMaps.unloadMap", Id);
        }

        [JSInvokable]
        public async Task NotifyMapLoaded()
        {
            if (Modules != null)
            {
                foreach (var module in Modules)
                {
                    await module.InitAsync(Id);
                }
            }
            MapLoaded?.Invoke();
            modulesLoaded = true;
            StateHasChanged();
        }
    }
}
