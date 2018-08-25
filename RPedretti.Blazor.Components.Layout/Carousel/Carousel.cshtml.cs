using Microsoft.AspNetCore.Blazor.Components;

namespace RPedretti.Blazor.Components.Layout.Carousel
{
    public class CarouselBase : BlazorComponent
    {
        #region Properties

        protected bool HasNext { get; set; }
        protected bool HasPrev { get; set; }

        #endregion Properties

        #region Methods

        public void GoToNext()
        {
        }

        public void GoToPrev()
        {
        }

        #endregion Methods
    }
}
