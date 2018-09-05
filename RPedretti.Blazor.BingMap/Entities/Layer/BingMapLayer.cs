using Microsoft.JSInterop;
using System;
using System.Collections;
using System.ComponentModel;

namespace RPedretti.Blazor.BingMap.Entities.Layer
{
    public class BingMapLayer : BaseBingMapEntity, IEnumerable
    {
        private const string layerNamespace = "rpedrettiBlazorComponents.bingMaps.layer";
        private readonly Shared.Collections.BindingList<BaseBingMapEntity> _items = new Shared.Collections.BindingList<BaseBingMapEntity>();
        private DotNetObjectRef thisRef;

        public BingMapLayer(string id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            thisRef = new DotNetObjectRef(this);
            _items.ListChanged += ItemsChanged;
            _items.BeforeRemove += ItemRemoved;
            JSRuntime.Current.InvokeAsync<object>(layerNamespace + ".init", Id, thisRef);
        }

        public void Add(BaseBingMapEntity item)
        {
            _items.Add(item);
        }

        public void Remove(BaseBingMapEntity item)
        {
            _items.Remove(item);
        }

        private void ItemRemoved(object sender, BaseBingMapEntity e)
        {
            JSRuntime.Current.InvokeAsync<object>(layerNamespace + ".removeItem", Id, e.Id);
        }

        public override void Dispose()
        {
            JSRuntime.Current.UntrackObjectRef(thisRef);
        }

        private void ItemsChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    JSRuntime.Current.InvokeAsync<object>(layerNamespace + ".addItem", Id, _items[e.NewIndex]);
                    break;
                case ListChangedType.ItemChanged:
                    break;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
