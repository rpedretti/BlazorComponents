using BlazorApp30.Domain;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace BlazorApp30.Components.MoviePoster
{
    public class MoviePosterBase : BlazorComponent
    {
        [Parameter]
        protected Action OnClick { get; set; }

        [Parameter]
        protected Movie Movie { get; set; }

        [Parameter]
        protected bool Flipped { get; set; }

        [Parameter]
        protected bool CanFlip { get; set; }

        protected bool ImageLoaded = false;
        protected bool ImageError;

        protected void HandleClick()
        {
            if (OnClick != null)
            {
                OnClick();
            }
            else if (CanFlip)
            {
                Flip();
            }
        }

        protected void Flip()
        {
            Flipped = !Flipped;
        }

        protected void HandleKeyPress(UIKeyboardEventArgs args)
        {
            if (CanFlip)
            {
                Console.WriteLine(args.Key);
                if (args.Key == " " || args.Key == "Enter")
                {
                    Flip();
                }
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
