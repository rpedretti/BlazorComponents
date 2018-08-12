using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
