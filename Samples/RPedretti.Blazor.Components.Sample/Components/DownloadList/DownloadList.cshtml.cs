using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components.Sample.Managers;
using RPedretti.Blazor.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.Sample.Components.DownloadList
{
    public class DownloadListBase : BaseAccessibleComponent, IDisposable
    {
        #region Methods

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

        #endregion Methods

        #region Fields

        protected List<DownloadResult> AvailableDownloads = new List<DownloadResult>();

        #endregion Fields

        #region Properties

        [Inject] protected DownloadManager DownloadManager { get; set; }

        protected string FinalPosition => "-" + Position.ToString().ToLower().Replace("_", "-");
        [Parameter] protected DownloadListPosition Position { get; set; } = DownloadListPosition.BOTTOM_RIGHT;

        #endregion Properties

        protected override void OnInit()
        {
            DownloadManager.NewDownloadAvailable += ShowDownloads;
            DownloadManager.DownloadRemoved += RemoveDownload;
        }

        protected async Task RemoveDownloadFromList(DownloadResult download)
        {
            await DownloadManager.RequestDownloadRemoveAsync(download.Id);
        }

        public void Dispose()
        {
            DownloadManager.NewDownloadAvailable -= ShowDownloads;
        }
    }
}
