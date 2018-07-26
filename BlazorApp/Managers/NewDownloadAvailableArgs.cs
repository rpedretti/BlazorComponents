using BlazorApp.Shared.Domain;
using System;

namespace BlazorApp.Managers
{
    public class NewDownloadAvailableArgs : EventArgs
    {
        public DownloadResult DownloadResult { get; set; }
    }
}
