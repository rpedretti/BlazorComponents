using RPedretti.Blazor.BingMap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPedretti.Blazor.BingMap.Collections
{
    public abstract class BaseBingMapEntityList<T> : System.ComponentModel.BindingList<T> where T : BaseBingMapEntity
    {
        public event EventHandler<T> BeforeRemove;
        public event EventHandler<IEnumerable<T>> BeforeRemoveRange;
        public event EventHandler<RangeChangdEventArgs> ListRangeChanged;

        protected override void RemoveItem(int index)
        {
            T deletedItem = Items[index];
            BeforeRemove?.Invoke(this, deletedItem);
            base.RemoveItem(index);
        }

        public void RemoveRange(int start, int ammount)
        {
            if (start + ammount > Items.Count)
            {
                throw new IndexOutOfRangeException();
            }

            var oldRaiseEventsValue = RaiseListChangedEvents;
            var items = new List<T>();

            try
            {
                RaiseListChangedEvents = false;
                for (int index = start; index < start + ammount; index++)
                {
                    items.Add(Items[index]);
                    base.RemoveItem(index);
                }
                BeforeRemoveRange?.Invoke(this, items);

            }
            finally
            {
                RaiseListChangedEvents = oldRaiseEventsValue;
                ListRangeChanged?.Invoke(this, new RangeChangdEventArgs(start, ammount, RangeChangeType.Remove));
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var oldRaiseEventsValue = RaiseListChangedEvents;
            var start = this.Items.Count;
            var ammount = collection.Count();

            try
            {
                RaiseListChangedEvents = false;
                
                foreach (var value in collection)
                {
                    Add(value);
                }
            }
            finally
            {
                RaiseListChangedEvents = oldRaiseEventsValue;
                ListRangeChanged?.Invoke(this, new RangeChangdEventArgs(start, ammount, RangeChangeType.Add));
            }
        }
    }
}
