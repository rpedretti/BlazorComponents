using BlazorApp30.ViewModel;
using Microsoft.AspNetCore.Blazor.Components;
using System.Threading.Tasks;

namespace BlazorApp30.Pages
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
