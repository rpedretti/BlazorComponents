using Blazor.Extensions;
using BlazorApp.Shared.Domain;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Managers
{
    public class DownloadManager
    {
        #region Fields

        private HubConnection connection;
        private IDisposable downloadRemovedSubscription;
        private IDisposable proccessFinishedSubscription;

        #endregion Fields

        #region Methods

        private async Task DownloaAvailable(DownloadResult result)
        {
            await AddResultToList(result);
        }

        private Task RemoveDownload(string id)
        {
            DownloadRemoved?.Invoke(this, new DownloadRemovedArgs { DownloadId = id });
            return Task.CompletedTask;
        }

        #endregion Methods

        #region Events

        public event EventHandler<DownloadRemovedArgs> DownloadRemoved;

        public event EventHandler<NewDownloadAvailableArgs> NewDownloadAvailable;

        #endregion Events

        public Task AddResultToList(DownloadResult result)
        {
            NewDownloadAvailable?.Invoke(this, new NewDownloadAvailableArgs() { DownloadResult = result });
            return Task.CompletedTask;
        }

        public async Task RequestDownloadRemoveAsync(string id)
        {
            await connection.InvokeAsync("RemoveFromDownloads", id);
        }

        public async Task RequestLongRunningProcess()
        {
            var guid = Guid.NewGuid();
            await connection.InvokeAsync("RequestLongProcessTaskAsync", guid);
        }

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
    }
}
