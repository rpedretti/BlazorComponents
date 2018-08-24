using RPedretti.Blazor.Components.Sample.Managers;
using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.Sample.Pages.SignalR
{
    public class SignalRBase : BaseComponent
    {
        #region Properties

        [Inject] private DownloadManager DownloadManager { get; set; }
        [Inject] private BlazorHubConnectionManager HubConnectionManager { get; set; }

        protected bool HasToken { get; set; }
        protected string Password { get; set; } = "bla";
        protected string Username { get; set; } = "rafa";

        #endregion Properties

        #region Methods

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

        protected override void OnInit()
        {
            HasToken = HubConnectionManager.IsConnected;
            base.OnInit();
        }

        protected async Task RequestLongProcessAsync()
        {
            await DownloadManager.RequestLongRunningProcess();
        }

        #endregion Methods
    }
}
