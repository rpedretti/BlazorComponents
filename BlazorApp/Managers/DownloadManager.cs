using Blazor.Extensions;
using BlazorApp.Shared.Domain;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Managers
{
    public class DownloadManager
    {
        private HubConnection connection;
        private IDisposable proccessFinishedSubscription;
        private IDisposable downloadRemovedSubscription;

        public event EventHandler<NewDownloadAvailableArgs> NewDownloadAvailable;
        public event EventHandler<DownloadRemovedArgs> DownloadRemoved;

        public void SetConnection(HubConnection connection)
        {
            if (proccessFinishedSubscription != null)
            {
                proccessFinishedSubscription.Dispose();
                downloadRemovedSubscription.Dispose();
            }

            this.connection = connection;
            proccessFinishedSubscription = connection.On<DownloadResult>("LongProcessFinished", DownloaAvailable);
            downloadRemovedSubscription = connection.On<string>("DownloadRemoved", RemoveDownload);
        }

        private Task RemoveDownload(string id)
        {
            DownloadRemoved?.Invoke(this, new DownloadRemovedArgs { DownloadId = id });
            return Task.CompletedTask;
        }

        private async Task DownloaAvailable(DownloadResult result)
        {
            await AddResultToList(result);
        }

        public async Task RequestLongRunningProcess()
        {
            var guid = Guid.NewGuid();
            await connection.InvokeAsync("RequestLongProcessTaskAsync", guid);
        }

        public async Task RequestDownloadRemoveAsync(string id)
        {
            await connection.InvokeAsync("RemoveFromDownloads", id);
        }

        public Task AddResultToList(DownloadResult result)
        {
            NewDownloadAvailable?.Invoke(this, new NewDownloadAvailableArgs() { DownloadResult = result });
            return Task.CompletedTask;
        }
    }
}
