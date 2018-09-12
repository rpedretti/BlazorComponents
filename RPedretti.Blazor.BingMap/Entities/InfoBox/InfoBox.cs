using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Entities.InfoBox
{
    public partial class InfoBox : BaseBingMapEntity
    {
        private string _htmlContent;
        private InfoboxOptions _options;
        private const string _mapsNamespace = "rpedrettiBlazorComponents.bingMaps";
        private const string _infoboxNamespace = "rpedrettiBlazorComponents.bingMaps.infobox";
        private const string _infoboxRegister = _infoboxNamespace + ".register";
        private const string _infoboxGet = _infoboxNamespace + ".get";
        private const string _infoboxSet = _infoboxNamespace + ".set";
        public Geocoordinate Center { get; set; }

        private DotNetObjectRef thisRef;

        public InfoBox(Geocoordinate center) : this(center, null, Guid.NewGuid().ToString()) { }
        public InfoBox(Geocoordinate center, InfoboxOptions options) : this(center, options, Guid.NewGuid().ToString()) { }
        public InfoBox(Geocoordinate center, InfoboxOptions options, string id)
        {
            Id = Guid.NewGuid().ToString();
            Center = center;
            JSRuntime.Current.InvokeAsync<object>(_infoboxRegister, Id, center, options);
            thisRef = new DotNetObjectRef(this);
        }

        public async Task<InfoboxAction> Actions() => await JSRuntime.Current.InvokeAsync<InfoboxAction>(_infoboxGet, Id, nameof(Actions));
        public async Task<GeolocatonPoint> Anchor() => await JSRuntime.Current.InvokeAsync<GeolocatonPoint>(_infoboxGet, Id, nameof(Actions));
        public async Task<string> Description() => await JSRuntime.Current.InvokeAsync<string>(_infoboxGet, Id, nameof(Actions));
        public async Task<double> Height() => await JSRuntime.Current.InvokeAsync<double>(_infoboxGet, Id, nameof(Actions));
        public async Task<string> HtmlContent() => await JSRuntime.Current.InvokeAsync<string>(_infoboxGet, Id, nameof(Actions));
        public async Task HtmlContent(string content)
        {
            if (SetParameter(ref _htmlContent, content))
            {
                await JSRuntime.Current.InvokeAsync<string>(_infoboxSet + nameof(HtmlContent), Id, content);
            }
        }
        public async Task<Location> Location() => await JSRuntime.Current.InvokeAsync<Location>(_infoboxGet, Id, nameof(Actions));
        public async Task Location(Location location) => await JSRuntime.Current.InvokeAsync<Location>(_infoboxSet + nameof(Location), Id, location);
        public async Task<double> MaxHeight() => await JSRuntime.Current.InvokeAsync<double>(_infoboxGet, Id, nameof(Actions));
        public async Task<double> MaxWidth() => await JSRuntime.Current.InvokeAsync<double>(_infoboxGet, Id, nameof(Actions));
        public async Task<GeolocatonPoint> Offset() => await JSRuntime.Current.InvokeAsync<GeolocatonPoint>(_infoboxGet, Id, nameof(Actions));
        public async Task<InfoboxOptions> Options() => await JSRuntime.Current.InvokeAsync<InfoboxOptions>(_infoboxGet, Id, nameof(Actions));
        public async Task Options(InfoboxOptions options)
        {
            if (SetParameter(ref _options, options))
            {
                await JSRuntime.Current.InvokeAsync<object>(_infoboxSet + nameof(Options), Id, options);
            }
        }
        public async Task<bool> ShowCloseButton() => await JSRuntime.Current.InvokeAsync<bool>(_infoboxGet, Id, nameof(Actions));
        public async Task AttachMap(string mapId) => await JSRuntime.Current.InvokeAsync<bool>(_mapsNamespace + ".attachInfoBox", mapId, Id);
        public async Task<bool> ShowPointer() => await JSRuntime.Current.InvokeAsync<bool>(_infoboxGet, Id, nameof(Actions));
        public async Task<string> Title() => await JSRuntime.Current.InvokeAsync<string>(_infoboxGet, Id, nameof(Actions));
        public async Task<bool> Visible() => await JSRuntime.Current.InvokeAsync<bool>(_infoboxGet, Id, nameof(Actions));
        public async Task<double> Width() => await JSRuntime.Current.InvokeAsync<double>(_infoboxGet, Id, nameof(Actions));
        public async Task<int> ZIndex() => await JSRuntime.Current.InvokeAsync<int>(_infoboxGet, Id, nameof(Actions));

        public override void Dispose()
        {
            JSRuntime.Current.UntrackObjectRef(thisRef);
        }
    }
}
