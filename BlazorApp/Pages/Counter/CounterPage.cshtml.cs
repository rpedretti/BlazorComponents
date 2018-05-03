using BlazorApp.ViewModel;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp.Pages
{
    public class CounterPageBase: BlazorComponent
    {
        [Inject]
        public CounterPageViewModel ViewModel { get; set; }
    }
}
