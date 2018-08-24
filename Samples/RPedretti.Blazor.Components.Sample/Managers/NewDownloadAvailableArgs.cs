using RPedretti.Blazor.Shared.Domain;
using System;

namespace RPedretti.Blazor.Components.Sample.Managers
{
    public class NewDownloadAvailableArgs : EventArgs
    {
        #region Properties

        public DownloadResult DownloadResult { get; set; }

        #endregion Properties
    }
}
