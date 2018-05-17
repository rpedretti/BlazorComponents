using System;
using System.Drawing;

namespace BlazorApp30.ViewModel
{
    public class IndexPageViewModel
    {
        public Color ParentBgColor { get; set; }
        public Color ChildBgColor { get; set; }
        public string ParagraphStyle { get; set; }
        public bool ExpandLoaderAccordeon { get; set; }

        public void OnComponentHover()
        {
            Console.WriteLine("Hover");
        }
    }
}
