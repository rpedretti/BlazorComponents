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

        protected Dictionary<int, bool> _flipped = new Dictionary<int, bool>();

        protected override async Task OnInitAsync()
        {
            await ViewModel.GetMoviesAsync();
            for (int i = 0; i < ViewModel.Model.Movies.Count; i++)
            {
                _flipped[i] = false;
            }

        }

        protected void HandleKeyPress(UIKeyboardEventArgs args, int index)
        {
            Console.WriteLine(args.Key);
            if (args.Key == " " || args.Key == "Enter")
            {
                Flip(index);
            }
        }

        protected void Flip(int index)
        {
            _flipped[index] = !_flipped[index];
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
