using BlazorApp.ViewModel;
using Microsoft.AspNetCore.Blazor.Components;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public class FetchDataBase : BlazorComponent
    {
        [Inject]
        public FetchDataPageViewModel ViewModel { get; set; }

        protected override async Task OnInitAsync()
        {
            await ViewModel.FetchDataAsync();
        }
    }
}
