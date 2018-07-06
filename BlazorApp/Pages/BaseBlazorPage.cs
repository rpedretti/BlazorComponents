using System.Collections.Generic;

namespace BlazorApp.Pages
{
    public abstract class BaseBlazorPage : Blazor.Fluxor.Temporary.FluxorComponent
    {
        #region Methods

        protected bool SetParameter<T>(ref T prop, T value)
        {
            if (EqualityComparer<T>.Default.Equals(prop, value))
            {
                return false;
            }

            prop = value;
            StateHasChanged();

            return true;
        }

        #endregion Methods
    }
}
