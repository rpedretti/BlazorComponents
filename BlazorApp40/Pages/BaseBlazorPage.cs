using Microsoft.AspNetCore.Blazor.Components;
using System.Collections.Generic;

namespace BlazorApp40.Pages
{
    public abstract class BaseBlazorPage : BlazorComponent
    {
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
    }
}
