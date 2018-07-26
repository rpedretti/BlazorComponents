using System;

namespace BlazorApp.Managers
{
    public class DownloadRemovedArgs : EventArgs
    {
        public string DownloadId { get; set; }
    }
}
