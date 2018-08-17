using Microsoft.AspNetCore.Blazor.Components;

namespace RPedretti.Blazor.Components.Carousel
{
    public class CarouselBase : BlazorComponent
    {
        protected bool HasNext { get; set; }
        protected bool HasPrev { get; set; }

        public void GoToNext()
        {

        }

        public void GoToPrev()
        {

        }
    }
}
