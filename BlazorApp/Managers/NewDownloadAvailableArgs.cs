using BlazorApp.Shared.Domain;
using System;

namespace BlazorApp.Managers
{
    public class NewDownloadAvailableArgs : EventArgs
    {
        #region Properties

        public DownloadResult DownloadResult { get; set; }

        #endregion Properties
    }
}
