using System;

namespace RPedretti.Blazor.Shared.Collections
{
    public class BindingList<T> : System.ComponentModel.BindingList<T>
    {
        public event EventHandler<T> BeforeRemove;

        protected override void RemoveItem(int index)
        {
            T deletedItem = this.Items[index];
            BeforeRemove?.Invoke(this, deletedItem);
            base.RemoveItem(index);
        }
    }
}
