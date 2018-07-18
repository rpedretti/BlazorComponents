using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.Blazor.SignalRServer.Hubs
{
    public class BlazorHub : Hub
    {
        private Dictionary<Guid, Timer> timers = new Dictionary<Guid, Timer>();

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("connected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async Task NotifyConnected(string username)
        {
            await Clients.Others.SendAsync("GuestEntered", username);
        }

        public Task RequestLongProcessTaskAsync(string id)
        {
            var guid = Guid.NewGuid();
            var timer = new Timer(o =>
            {
                (o as IClientProxy).SendAsync("LongProcessFinished", new { Id = id, Url = "http://pudim.com.br" });
                timers[guid].Dispose();
                timers.Remove(guid);
            }, Clients.Caller, 5000, Timeout.Infinite);

            timers[guid] = timer;

            return Task.CompletedTask;
        }
    }
}
