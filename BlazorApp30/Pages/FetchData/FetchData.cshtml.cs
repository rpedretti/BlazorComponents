using BlazorApp30.ViewModel;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Threading.Tasks;

namespace BlazorApp30.Pages
{
    public class FetchDataBase : BlazorComponent, IDisposable
    {
        [Inject]
        public FetchDataPageViewModel ViewModel { get; set; }

        protected override async Task OnInitAsync()
        {
            ViewModel.StateHasChanged += Update;
            await ViewModel.FetchDataAsync();
            StateHasChanged();
        }

        public void Dispose()
        {
            ViewModel.StateHasChanged -= Update;
        }

        private void Update(object sender, EventArgs e)
        {
            StateHasChanged();
        }
    }
}
