using System;
using System.ComponentModel;

namespace RPedretti.Blazor.BingMaps.Entities.Layer
{
    public class BingMapLayer : BaseBingMapEntity
    {
        private BindingList<BaseBingMapEntity> _items;

        public BingMapLayer(string id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
        }

        protected BindingList<BaseBingMapEntity> Items
        {
            get => _items;
            set
            {
                if (_items != null) _items.ListChanged -= ItemsChanged;

                _items = value;

                if (_items != null) _items.ListChanged += ItemsChanged;
            }
        }

        public override void Dispose()
        {
            
        }

        private void ItemsChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    break;
                case ListChangedType.ItemChanged:
                    break;
                case ListChangedType.ItemDeleted:
                    break;
                case ListChangedType.ItemMoved:
                    break;
                case ListChangedType.PropertyDescriptorAdded:
                    break;
                case ListChangedType.PropertyDescriptorChanged:
                    break;
                case ListChangedType.PropertyDescriptorDeleted:
                    break;
                case ListChangedType.Reset:
                    break;
                default:
                    break;
            }
        }
    }
}
