using BlazorApp.Managers;
using BlazorApp.Shared.Domain;
using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Components.DownloadList
{
    public class DownloadListBase : BaseAccessibleComponent, IDisposable
    {
        [Inject] protected DownloadManager DownloadManager { get; set; }

        [Parameter] protected DownloadListPosition Position { get; set; } = DownloadListPosition.BOTTOM_RIGHT;

        protected List<DownloadResult> AvailableDownloads = new List<DownloadResult>();

        protected override void OnInit()
        {
            DownloadManager.NewDownloadAvailable += ShowDownloads;
            DownloadManager.DownloadRemoved += RemoveDownload;
        }

        protected async Task RemoveDownloadFromList(DownloadResult download)
        {
            await DownloadManager.RequestDownloadRemoveAsync(download.Id);
        }

        private void RemoveDownload(object sender, DownloadRemovedArgs e)
        {
            var download = AvailableDownloads?.FirstOrDefault(d => d.Id == e.DownloadId);
            if (download != null)
            {
                AvailableDownloads.Remove(download);
                StateHasChanged();
            }
        }

        private void ShowDownloads(object sender, NewDownloadAvailableArgs e)
        {
            var result = e.DownloadResult;
            result.Description = $"{result.Url} ({result.Id})";
            AvailableDownloads.Add(result);
            StateHasChanged();
        }

        public new void Dispose()
        {
            DownloadManager.NewDownloadAvailable -= ShowDownloads;
            base.Dispose();
        }

        protected string FinalPosition => "-" + Position.ToString().ToLower().Replace("_", "-");
    }
}
