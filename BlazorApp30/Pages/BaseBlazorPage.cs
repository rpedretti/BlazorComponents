using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp30.Pages
{
    public abstract class BaseBlazorPage : BlazorComponent
    {
        protected void SetParameter<T>(ref T prop, T value)
        {
            prop = value;
            StateHasChanged();
        }
    }
}
