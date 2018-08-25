using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RPedretti.Blazor.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.Blazor.SignalRServer.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BlazorHub : Hub
    {
        #region Fields

        private static Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
        private static Dictionary<string, List<DownloadResult>> downloads = new Dictionary<string, List<DownloadResult>>();
        private static Dictionary<Guid, Timer> timers = new Dictionary<Guid, Timer>();

        #endregion Fields

        #region Classes

        private class TaskId
        {
            #region Properties

            public string Id { get; set; }
            public IClientProxy User { get; set; }

            #endregion Properties
        }

        #endregion Classes

        #region Methods

        public override async Task OnConnectedAsync()
        {
            if (!connections.ContainsKey(Context.UserIdentifier))
            {
                connections[Context.UserIdentifier] = new List<string>();
            }
            connections[Context.UserIdentifier].Add(Context.ConnectionId);

            await Clients.AllExcept(connections[Context.UserIdentifier]).SendAsync("GuestEntered", Context.UserIdentifier);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            connections[Context.UserIdentifier].Remove(Context.ConnectionId);
            if (!connections[Context.UserIdentifier].Any())
            {
                await Clients.Others.SendAsync("GuestLeft", Context.UserIdentifier);
            }
        }

        public async Task RemoveFromDownloads(string id)
        {
            var download = downloads[Context.UserIdentifier].FirstOrDefault();
            if (download != null)
            {
                downloads[Context.UserIdentifier].Remove(download);
                await Clients.User(Context.UserIdentifier).SendAsync("DownloadRemoved", id);
            }
        }

        public Task RequestLongProcessTaskAsync(string id)
        {
            var guid = Guid.NewGuid();
            var timer = new Timer(async o =>
            {
                var userIdentifyer = o as TaskId;
                var result = new DownloadResult { Id = id, Url = "http://pudim.com.br" };
                if (!downloads.ContainsKey(userIdentifyer.Id))
                {
                    downloads[userIdentifyer.Id] = new List<DownloadResult>();
                }
                downloads[userIdentifyer.Id].Add(result);
                await userIdentifyer.User.SendAsync("LongProcessFinished", result);
                timers[guid].Dispose();
                timers.Remove(guid);
            }, new TaskId { Id = Context.UserIdentifier, User = Clients.User(Context.UserIdentifier) }, 5000, Timeout.Infinite);

            timers[guid] = timer;

            return Task.CompletedTask;
        }

        #endregion Methods
    }
}
