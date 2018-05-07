using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.ViewModel
{
    public class IndexPageViewModel
    {
        public Color ParentBgColor { get; set; }
        public Color ChildBgColor { get; set; }

        public void OnComponentHover()
        {
            Console.WriteLine("Hover");
        }
    }
}
