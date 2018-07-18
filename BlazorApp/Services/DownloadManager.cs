using Blazor.Extensions;
using BlazorApp.Domain;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class DownloadManager
    {
        private readonly Dictionary<Guid, Action<DownloadResult>> downloadMap = new Dictionary<Guid, Action<DownloadResult>>();
        private readonly HubConnection connection;

        public event EventHandler<NewDownloadAvailableArgs> NewDownloadAvailabe;

        public DownloadManager(HubConnection connection)
        {
            this.connection = connection;

            connection.On("LongProcessFinished", NewMethod);
        }

        private Task NewMethod(object o)
        {
            Console.WriteLine(o.ToString());
            var deserialized = JsonUtil.Deserialize<DownloadResult>(o.ToString());
            var guid = Guid.Parse(deserialized.Id);
            if (downloadMap.ContainsKey(guid))
            {
                downloadMap[guid](deserialized);
                downloadMap.Remove(guid);
            }
            return Task.CompletedTask;
        }

        public async Task RequestLongRunningProcess()
        {
            var guid = Guid.NewGuid();
            void instanceFunc(DownloadResult result)
            {
                AddResultToList(result);
            }
            downloadMap[guid] = instanceFunc;
            await connection.InvokeAsync("RequestLongProcessTaskAsync", guid);
        }

        public Task AddResultToList(DownloadResult result)
        {
            Console.WriteLine($"ended {JsonUtil.Serialize(result)}");
            NewDownloadAvailabe?.Invoke(this, new NewDownloadAvailableArgs() { DownloadResult = result });
            return Task.CompletedTask;
        }
    }
}
