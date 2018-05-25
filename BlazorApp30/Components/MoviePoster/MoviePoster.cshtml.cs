using BlazorApp30.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace BlazorApp30.Components.MoviePoster
{
    public class MoviePosterBase : BaseAccessibleComponent
    {
        [Parameter] protected Action OnClick { get; set; }

        [Parameter] protected MoviePosterModel Movie { get; set; }

        protected bool ImageLoaded = false;
        protected bool ImageError;

        protected void HandleClick()
        {
            OnClick?.Invoke();
        }

        protected void HandleKeyPress(UIKeyboardEventArgs args)
        {
            if (args.Key == " " || args.Key == "Enter")
            {
                OnClick?.Invoke();
            }
        }

        protected void UpdateLoader()
        {
            ImageLoaded = true;
            StateHasChanged();
        }

        protected void UpdateError()
        {
            ImageError = true;
            ImageLoaded = true;
            StateHasChanged();
        }
    }
}
