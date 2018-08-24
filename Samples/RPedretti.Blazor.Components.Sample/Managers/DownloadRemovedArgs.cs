using System;

namespace RPedretti.Blazor.Components.Sample.Managers
{
    public class DownloadRemovedArgs : EventArgs
    {
        #region Properties

        public string DownloadId { get; set; }

        #endregion Properties
    }
}
