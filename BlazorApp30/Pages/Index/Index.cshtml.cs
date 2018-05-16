using System.Drawing;
using Microsoft.AspNetCore.Blazor.Components;
using BlazorApp30.ViewModel;

namespace BlazorApp30.Pages.Index
{
    public class IndexBase : BlazorComponent
    {
        [Inject]
        protected IndexPageViewModel ViewModel { get; set; }

        protected override void OnInit()
        {
            ViewModel.ParentBgColor = Color.AliceBlue;
            ViewModel.ChildBgColor = Color.FromArgb(220, 111, 25, 111);
            ViewModel.ParagraphStyle = "background: ";
        }
    }
}
