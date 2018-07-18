using BlazorApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class NewDownloadAvailableArgs : EventArgs
    {
        public DownloadResult DownloadResult { get; set; }
    }
}
