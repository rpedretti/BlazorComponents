using BlazorApp30.ViewModel;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp30.Pages
{
    public class TodoBase : BlazorComponent
    {
        [Inject]
        protected TodoPageViewModel ViewModel { get; set; }
    }
}
