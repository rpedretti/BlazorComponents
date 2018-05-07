using BlazorApp30.ViewModel;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorApp30.Pages
{
    public class CounterPageBase: BlazorComponent
    {
        [Inject]
        public CounterPageViewModel ViewModel { get; set; }
    }
}
