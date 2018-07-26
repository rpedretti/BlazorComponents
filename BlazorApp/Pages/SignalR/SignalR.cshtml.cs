using Blazor.Extensions;
using BlazorApp.Managers;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor.Components;
using Newtonsoft.Json;
using RPedretti.Blazor.Shared.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Pages.SignalR
{
    public class SignalRBase : BaseBlazorPage
    {
        [Inject] private DownloadManager DownloadManager { get; set; }
        [Inject] private BlazorHubConnectionManager HubConnectionManager { get; set; }

        protected string Username { get; set; } = "rafa";
        protected string Password { get; set; } = "bla";
        protected bool HasToken { get; set; }

        protected override void OnInit()
        {
            HasToken = HubConnectionManager.IsConnected;
            base.OnInit();
        }

        protected async Task RequestLongProcessAsync()
        {
            await DownloadManager.RequestLongRunningProcess();
        }

        protected async Task LoginAsync()
        {
            var connection = await HubConnectionManager.ConnectAsync(Username, Password);
            HasToken = true;
            DownloadManager.SetConnection(connection);
            StateHasChanged();
        }

        protected async Task LogoutAsync()
        {
            await HubConnectionManager.CloseConnectionAsync();
            HasToken = false;
            StateHasChanged();
        }

    }
}
