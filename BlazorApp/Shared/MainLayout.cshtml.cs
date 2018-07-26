using Blazor.Fluxor;
using BlazorApp.Managers;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using System.Collections.Generic;

namespace BlazorApp.Shared
{
    public class MainLayoutBase : BlazorLayoutComponent
    {
        [Inject] protected IStore Store { get; set; }
        [Inject] private NotificationManager NotificationManager { get; set; }

        protected override void OnInit()
        {
            NotificationManager.ShowNotification += ShowNotification;
        }

        private void ShowNotification(object sender, NotificationManager.NotificationEventArgs e)
        {
            Messages.Add(e.Message);
            StateHasChanged();
        }

        protected List<string> Messages = new List<string>();
    }
}
