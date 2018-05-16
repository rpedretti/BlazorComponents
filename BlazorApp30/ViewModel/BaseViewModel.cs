using System;

namespace BlazorApp30.ViewModel
{
    public abstract class BaseViewModel
    {
        public event EventHandler StateHasChanged;

        protected virtual void OnStateHasChanged()
        {
            StateHasChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}