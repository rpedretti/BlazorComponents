using System.Drawing;

namespace RPedretti.Blazor.BingMaps.Entities
{
    public class BingMapsConfig : BingMapsViewConfig
    {
        #region Properties

        public bool? AllowHidingLabelsOfRoad { get; set; }
        public bool? AllowInfoboxOverflow { get; set; }
        public Color? BackgroundColor { get; set; }
        public string Credentials { get; set; }
        public bool? DisableBirdseye { get; set; }
        public bool? DisableKeyboardInput { get; set; }
        public bool? DisableMapTypeSelectorMouseOver { get; set; }
        public bool? DisablePanning { get; set; }
        public bool? DisableScrollWheelZoom { get; set; }
        public bool? DisableStreetside { get; set; }
        public bool? DisableStreetsideAutoCoverage { get; set; }
        public bool? DisableZooming { get; set; }
        public bool? EnableClickableLogo { get; set; }
        public bool? EnableCORS { get; set; }
        public bool? EnableHighDpi { get; set; }
        public bool? EnableInertia { get; set; }
        public bool? LiteMode { get; set; }
        public LocationRectangle MaxBounds { get; set; }
        public int? MaxZoom { get; set; }
        public int? MinZoom { get; set; }
        public byte? NavigationBarMode { get; set; }
        public byte? NavigationBarOrientation { get; set; }
        public bool? ShowBreadcrumb { get; set; }
        public bool? ShowDashboard { get; set; }
        public bool? ShowLocateMeButton { get; set; }
        public bool? ShowMapTypeSelector { get; set; }
        public bool? ShowScalebar { get; set; }
        public bool? ShowTermsLink { get; set; }
        public bool? ShowTrafficButton { get; set; }
        public bool? ShowZoomButtons { get; set; }
        public StreetsideOptions StreetsideOptions { get; set; }
        public string[] SupportedMapTypes { get; set; }

        #endregion Properties
    }
}
