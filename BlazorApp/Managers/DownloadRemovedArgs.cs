using System;

namespace BlazorApp.Managers
{
    public class DownloadRemovedArgs : EventArgs
    {
        #region Properties

        public string DownloadId { get; set; }

        #endregion Properties
    }
}
