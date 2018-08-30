using Microsoft.JSInterop;

namespace RPedretti.Blazor.BingMaps.Entities.Pushpin
{
    public partial class BingMapPushpin : BaseBingMapEntity
    {
        #region Fields

        private Geocoordinate _center = new Geocoordinate();
        private PushpinOptions _options;
        private DotNetObjectRef thisRef;

        #endregion Fields

        #region Properties

        public Geocoordinate Center { get => _center; set => SetParameter(ref _center, value); }
        public PushpinOptions Options { get => _options; set => SetParameter(ref _options, value); }

        #endregion Properties
    }
}
