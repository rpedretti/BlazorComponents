using System.Drawing;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp30.Pages.Index
{
    public class IndexBase : BlazorComponent
    {

        public Color ParentBgColor { get; set; }
        public Color ChildBgColor { get; set; }
        public string ParagraphStyle { get; set; }
        public bool ExpandLoaderAccordeon { get; set; }

        public IndexBase()
        {
            ParentBgColor = Color.AliceBlue;
            ChildBgColor = Color.FromArgb(220, 111, 25, 111);
            ParagraphStyle = "background: ";
        }
    }
}
