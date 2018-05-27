using BlazorApp30.Components.PagedGrid;
using BlazorApp30.ViewModel;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp30.Pages.Movies
{
    public class MoviesBase : BlazorComponent
    {
        protected PagedGrid PagedGrid { get; set; }

        [Inject]
        protected MoviesPageViewModel ViewModel { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            ViewModel.StateHasChanged += NotifyChanged;
        }

        protected void HandleKeyPress(UIKeyboardEventArgs args, string id)
        {
            Console.WriteLine(args.Key);
            if (args.Key == " " || args.Key == "Enter")
            {
                ViewModel.GoToMovie(id);
            }
        }

        protected async void RequestPage(int page)
        {
            await ViewModel.GetMoviesAsync(page);
        }

        private void NotifyChanged(object sender, EventArgs e)
        {
            StateHasChanged();
        }
    }
}
