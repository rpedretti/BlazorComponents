using BlazorApp.Domain;
using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BlazorApp.Components.DownloadList
{
    public class DownloadListBase : BaseAccessibleComponent
    {
        private ObservableCollection<DownloadResult> _availableDownloads = new ObservableCollection<DownloadResult>();

        [Parameter] protected ObservableCollection<DownloadResult> AvailableDownloads
        {
            get => _availableDownloads;
            set
            {
                if (value != _availableDownloads)
                {
                    _availableDownloads.CollectionChanged -= UpdateList;
                }

                if (SetParameter(ref _availableDownloads, value))
                {
                    StateHasChanged();
                    if (value != null)
                        _availableDownloads.CollectionChanged += UpdateList;
                }
            }
        }

        private void UpdateList(object sender, NotifyCollectionChangedEventArgs e)
        {
            StateHasChanged();
        }

        [Parameter] protected DownloadListPosition Position { get; set; } = DownloadListPosition.BOTTOM_RIGHT;

        protected string FinalPosition => "-" + Position.ToString().ToLower().Replace("_", "-");
    }
}
