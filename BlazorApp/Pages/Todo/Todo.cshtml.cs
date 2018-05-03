using BlazorApp.ViewModel;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp.Pages
{
    public class TodoBase : BlazorComponent
    {
        [Inject]
        protected TodoPageViewModel ViewModel { get; set; }
    }
}
