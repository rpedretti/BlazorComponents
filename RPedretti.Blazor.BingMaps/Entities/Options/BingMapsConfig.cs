using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            var config = obj as BingMapsConfig;
            return config != null &&
                   base.Equals(obj) &&
                   EqualityComparer<bool?>.Default.Equals(AllowHidingLabelsOfRoad, config.AllowHidingLabelsOfRoad) &&
                   EqualityComparer<bool?>.Default.Equals(AllowInfoboxOverflow, config.AllowInfoboxOverflow) &&
                   EqualityComparer<Color?>.Default.Equals(BackgroundColor, config.BackgroundColor) &&
                   Credentials == config.Credentials &&
                   EqualityComparer<bool?>.Default.Equals(DisableBirdseye, config.DisableBirdseye) &&
                   EqualityComparer<bool?>.Default.Equals(DisableKeyboardInput, config.DisableKeyboardInput) &&
                   EqualityComparer<bool?>.Default.Equals(DisableMapTypeSelectorMouseOver, config.DisableMapTypeSelectorMouseOver) &&
                   EqualityComparer<bool?>.Default.Equals(DisablePanning, config.DisablePanning) &&
                   EqualityComparer<bool?>.Default.Equals(DisableScrollWheelZoom, config.DisableScrollWheelZoom) &&
                   EqualityComparer<bool?>.Default.Equals(DisableStreetside, config.DisableStreetside) &&
                   EqualityComparer<bool?>.Default.Equals(DisableStreetsideAutoCoverage, config.DisableStreetsideAutoCoverage) &&
                   EqualityComparer<bool?>.Default.Equals(DisableZooming, config.DisableZooming) &&
                   EqualityComparer<bool?>.Default.Equals(EnableClickableLogo, config.EnableClickableLogo) &&
                   EqualityComparer<bool?>.Default.Equals(EnableCORS, config.EnableCORS) &&
                   EqualityComparer<bool?>.Default.Equals(EnableHighDpi, config.EnableHighDpi) &&
                   EqualityComparer<bool?>.Default.Equals(EnableInertia, config.EnableInertia) &&
                   EqualityComparer<bool?>.Default.Equals(LiteMode, config.LiteMode) &&
                   EqualityComparer<LocationRectangle>.Default.Equals(MaxBounds, config.MaxBounds) &&
                   EqualityComparer<int?>.Default.Equals(MaxZoom, config.MaxZoom) &&
                   EqualityComparer<int?>.Default.Equals(MinZoom, config.MinZoom) &&
                   EqualityComparer<byte?>.Default.Equals(NavigationBarMode, config.NavigationBarMode) &&
                   EqualityComparer<byte?>.Default.Equals(NavigationBarOrientation, config.NavigationBarOrientation) &&
                   EqualityComparer<bool?>.Default.Equals(ShowBreadcrumb, config.ShowBreadcrumb) &&
                   EqualityComparer<bool?>.Default.Equals(ShowDashboard, config.ShowDashboard) &&
                   EqualityComparer<bool?>.Default.Equals(ShowLocateMeButton, config.ShowLocateMeButton) &&
                   EqualityComparer<bool?>.Default.Equals(ShowMapTypeSelector, config.ShowMapTypeSelector) &&
                   EqualityComparer<bool?>.Default.Equals(ShowScalebar, config.ShowScalebar) &&
                   EqualityComparer<bool?>.Default.Equals(ShowTermsLink, config.ShowTermsLink) &&
                   EqualityComparer<bool?>.Default.Equals(ShowTrafficButton, config.ShowTrafficButton) &&
                   EqualityComparer<bool?>.Default.Equals(ShowZoomButtons, config.ShowZoomButtons) &&
                   EqualityComparer<StreetsideOptions>.Default.Equals(StreetsideOptions, config.StreetsideOptions) &&
                   EqualityComparer<string[]>.Default.Equals(SupportedMapTypes, config.SupportedMapTypes);
        }

        public override int GetHashCode()
        {
            var hashCode = -138834109;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(AllowHidingLabelsOfRoad);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(AllowInfoboxOverflow);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color?>.Default.GetHashCode(BackgroundColor);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Credentials);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(DisableBirdseye);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(DisableKeyboardInput);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(DisableMapTypeSelectorMouseOver);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(DisablePanning);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(DisableScrollWheelZoom);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(DisableStreetside);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(DisableStreetsideAutoCoverage);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(DisableZooming);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(EnableClickableLogo);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(EnableCORS);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(EnableHighDpi);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(EnableInertia);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(LiteMode);
            hashCode = hashCode * -1521134295 + EqualityComparer<LocationRectangle>.Default.GetHashCode(MaxBounds);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(MaxZoom);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(MinZoom);
            hashCode = hashCode * -1521134295 + EqualityComparer<byte?>.Default.GetHashCode(NavigationBarMode);
            hashCode = hashCode * -1521134295 + EqualityComparer<byte?>.Default.GetHashCode(NavigationBarOrientation);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowBreadcrumb);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowDashboard);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowLocateMeButton);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowMapTypeSelector);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowScalebar);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowTermsLink);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowTrafficButton);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowZoomButtons);
            hashCode = hashCode * -1521134295 + EqualityComparer<StreetsideOptions>.Default.GetHashCode(StreetsideOptions);
            hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(SupportedMapTypes);
            return hashCode;
        }

        #endregion Properties
    }
}
