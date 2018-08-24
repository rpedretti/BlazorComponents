using RPedretti.Blazor.Components.Sample.Models;
using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components;
using System;

namespace RPedretti.Blazor.Components.Sample.Components.MoviePoster
{
    public class MoviePosterBase : BaseAccessibleComponent
    {
        #region Fields

        protected bool ImageError;
        protected bool ImageLoaded = false;

        #endregion Fields

        #region Properties

        [Parameter] protected MoviePosterModel Movie { get; set; }
        [Parameter] protected Action OnClick { get; set; }

        #endregion Properties

        #region Methods

        protected void HandleClick()
        {
            OnClick?.Invoke();
        }

        protected void UpdateError()
        {
            ImageError = true;
            ImageLoaded = true;
            StateHasChanged();
        }

        protected void UpdateLoader()
        {
            ImageLoaded = true;
            StateHasChanged();
        }

        #endregion Methods
    }
}
