using Blazor.Extensions;
using Newtonsoft.Json;
using RPedretti.Blazor.Shared.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Managers
{
    public class BlazorHubConnectionManager : IDisposable
    {
        #region Fields

#if DEBUG
        private readonly string baseUrl = "http://localhost:5000";
#else
        private readonly string baseUrl = "http://blazorsignalr.azurewebsites.net";
#endif
        private HubConnection connection;
        private HttpClient HttpClient;
        private TokenModel Jwt;
        private NotificationManager NotificationManager;

        #endregion Fields

        #region Constructors

        public BlazorHubConnectionManager(HttpClient httpClient, NotificationManager notificationManager)
        {
            HttpClient = httpClient;
            NotificationManager = notificationManager;
        }

        #endregion Constructors

        #region Properties

        public bool IsConnected => connection != null;

        #endregion Properties

        #region Methods

        public async Task<bool> CloseConnectionAsync()
        {
            await connection?.StopAsync();
            connection = null;
            return true;
        }

        public async Task<HubConnection> ConnectAsync(string username, string password)
        {
            if (IsConnected)
            {
                await CloseConnectionAsync();
            }

            HttpResponseMessage response;

            if (Jwt != null && Jwt.IsExpired)
            {
                Console.WriteLine("token expired");
                var refreshContent = new StringContent(Convert.ToBase64String(Encoding.UTF8.GetBytes(Jwt.RefreshToken)), Encoding.UTF8, "application/json");
                response = await HttpClient.PostAsync(Jwt.RefreshUrl, refreshContent);
                if (response.IsSuccessStatusCode)
                {
                    var serializedJwt = await response.Content.ReadAsStringAsync();
                    var jwt = JsonConvert.DeserializeObject<SecureJwtModel>(serializedJwt);
                    Jwt = jwt.TokenModel;
                }
            }
            else
            {
                var userModel = new UserAuthenticationModel { Username = username, Password = password };
                var content = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");
                response = await HttpClient.PostAsync($"{baseUrl}/jwt/requestjwt", content);

                if (response.IsSuccessStatusCode)
                {
                    var serializedJwt = await response.Content.ReadAsStringAsync();
                    var jwt = JsonConvert.DeserializeObject<SecureJwtModel>(serializedJwt);
                    Jwt = jwt.TokenModel;
                    connection = new HubConnectionBuilder()
                        .WithUrl($"{baseUrl}/blazorhub", opt =>
                        {
                            opt.AccessTokenProvider = () => Task.FromResult(Jwt.Token);
                            opt.LogLevel = SignalRLogLevel.Debug;
                            opt.SkipNegotiation = true;
                            opt.Transport = HttpTransportType.WebSockets;
                        })
                        .Build();

                    connection.On<string>("GuestEntered", id =>
                    {
                        NotificationManager.ShowNotificationMessage($"User {id} just entered", "New User");
                        return Task.CompletedTask;
                    });

                    connection.On<string>("GuestLeft", id =>
                    {
                        NotificationManager.ShowNotificationMessage($"User {id} just left", "New User");
                        return Task.CompletedTask;
                    });

                    connection.OnClose(ex =>
                    {
                        Console.WriteLine("Connection closed");
                        return Task.CompletedTask;
                    });

                    await connection.StartAsync();
                }
            }

            return connection;
        }

        public async void Dispose()
        {
            await CloseConnectionAsync();
        }

        #endregion Methods
    }
}
