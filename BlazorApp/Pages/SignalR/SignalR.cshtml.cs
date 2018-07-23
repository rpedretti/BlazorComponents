using Blazor.Extensions;
using BlazorApp.Services;
using Microsoft.AspNetCore.Blazor.Components;
using Newtonsoft.Json;
using RPedretti.Blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RPedretti.Security;

namespace BlazorApp.Pages.SignalR
{
    public class SignalRBase : BaseBlazorPage
    {
        [Inject] protected DownloadManager DownloadManager { get; set; }
        [Inject] protected HttpClient HttpClient { get; set; }

        protected string Username { get; set; } = "rafa";
        protected string Password { get; set; } = "bla";
        protected string Jwt { get; set; } = "";
        protected bool HasToken { get; set; } = false;
        private HubConnection connection;

        protected async Task RequestLongProcessAsync()
        {
            await DownloadManager.RequestLongRunningProcess();
        }

        protected async Task Login()
        {
#if DEBUG
            var baseUrl = "http://localhost:5000";
#else
            var baseUrl = "http://blazorsignalr.azurewebsites.net";
#endif
            var userModel = new UserAuthenticationModel { Username = Username, Password = Password };
            var content = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{baseUrl}/jwt/requestjwt", content);
            if (response.IsSuccessStatusCode)
            {
                HasToken = true;
                var serializedJwt = await response.Content.ReadAsStringAsync();
                var jwt = JsonConvert.DeserializeObject<SecureJwtModel>(serializedJwt);
                Jwt = jwt.TokenModel.Token;
                connection = new HubConnectionBuilder()
                    .WithUrl($"{baseUrl}/blazorhub", opt =>
                    {
                        opt.AccessTokenProvider = () => Task.FromResult(Jwt);
                        opt.LogLevel = SignalRLogLevel.Debug;
                        opt.SkipNegotiation = true;
                        opt.Transport = HttpTransportType.WebSockets;
                    })
                    .AddMessagePackProtocol()
                    .Build();


                connection.On<string>("connected", o => connection.InvokeAsync("NotifyConnected", o));
                DownloadManager.SetConnection(connection);
                await connection.StartAsync();
            }

            StateHasChanged();
        }
    }
}
