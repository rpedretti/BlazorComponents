using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BlazorHub : Hub
    {
        private Dictionary<Guid, Timer> timers = new Dictionary<Guid, Timer>();

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"user: {Context.UserIdentifier}");
            await Clients.Caller.SendAsync("connected", Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        public async Task NotifyConnected(string username)
        {
            await Clients.Others.SendAsync("GuestEntered", username);
        }

        public Task RequestLongProcessTaskAsync(string id)
        {
            Console.WriteLine($"user: {Context.UserIdentifier}");
            var guid = Guid.NewGuid();
            var timer = new Timer(o =>
            {
                (o as IClientProxy).SendAsync("LongProcessFinished", new { Id = id, Url = "http://pudim.com.br" });
                timers[guid].Dispose();
                timers.Remove(guid);
            }, Clients.User(Context.UserIdentifier), 5000, Timeout.Infinite);

            timers[guid] = timer;

            return Task.CompletedTask;
        }
    }
}
