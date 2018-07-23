using Blazor.Extensions;
using BlazorApp.Domain;
using Microsoft.AspNetCore.Blazor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class DownloadManager
    {
        private readonly Dictionary<Guid, Action<DownloadResult>> downloadMap = new Dictionary<Guid, Action<DownloadResult>>();
        private HubConnection connection;

        public event EventHandler<NewDownloadAvailableArgs> NewDownloadAvailabe;
        
        public void SetConnection(HubConnection connection)
        {
            if (this.connection != null)
            {
                //connection.Off();
            }

            this.connection = connection;
            connection.On<DownloadResult>("LongProcessFinished", NewMethod);
        }

        private async Task NewMethod(DownloadResult result)
        {
            Console.WriteLine(result);
            await AddResultToList(result);
        }

        public async Task RequestLongRunningProcess()
        {
            var guid = Guid.NewGuid();
            await connection.InvokeAsync("RequestLongProcessTaskAsync", guid);
        }

        public Task AddResultToList(DownloadResult result)
        {
            Console.WriteLine($"ended {JsonConvert.SerializeObject(result)}");
            NewDownloadAvailabe?.Invoke(this, new NewDownloadAvailableArgs() { DownloadResult = result });
            return Task.CompletedTask;
        }
    }
}
