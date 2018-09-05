using System.Collections.Generic;

namespace RPedretti.Blazor.BingMap.Entities
{
    public class StreetsideOptions
    {
        #region Properties

        public bool? DisablePanoramaNavigation { get; set; }
        public Geocoordinate LocationToLookAt { get; set; }
        public byte? OverviewMapMode { get; set; }
        public PanoramaInfo PanoramaInfo { get; set; }
        public double? PanoramaLookupRadius { get; set; }
        public bool? ShowCurrentAddress { get; set; }
        public bool? ShowExitButton { get; set; }
        public bool? ShowHeadingCompass { get; set; }
        public bool? ShowProblemReporting { get; set; }
        public bool? ShowZoomButtons { get; set; }

        public override bool Equals(object obj)
        {
            var options = obj as StreetsideOptions;
            return options != null &&
                   EqualityComparer<bool?>.Default.Equals(DisablePanoramaNavigation, options.DisablePanoramaNavigation) &&
                   EqualityComparer<Geocoordinate>.Default.Equals(LocationToLookAt, options.LocationToLookAt) &&
                   EqualityComparer<byte?>.Default.Equals(OverviewMapMode, options.OverviewMapMode) &&
                   EqualityComparer<PanoramaInfo>.Default.Equals(PanoramaInfo, options.PanoramaInfo) &&
                   EqualityComparer<double?>.Default.Equals(PanoramaLookupRadius, options.PanoramaLookupRadius) &&
                   EqualityComparer<bool?>.Default.Equals(ShowCurrentAddress, options.ShowCurrentAddress) &&
                   EqualityComparer<bool?>.Default.Equals(ShowExitButton, options.ShowExitButton) &&
                   EqualityComparer<bool?>.Default.Equals(ShowHeadingCompass, options.ShowHeadingCompass) &&
                   EqualityComparer<bool?>.Default.Equals(ShowProblemReporting, options.ShowProblemReporting) &&
                   EqualityComparer<bool?>.Default.Equals(ShowZoomButtons, options.ShowZoomButtons);
        }

        public override int GetHashCode()
        {
            var hashCode = 48974786;
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(DisablePanoramaNavigation);
            hashCode = hashCode * -1521134295 + EqualityComparer<Geocoordinate>.Default.GetHashCode(LocationToLookAt);
            hashCode = hashCode * -1521134295 + EqualityComparer<byte?>.Default.GetHashCode(OverviewMapMode);
            hashCode = hashCode * -1521134295 + EqualityComparer<PanoramaInfo>.Default.GetHashCode(PanoramaInfo);
            hashCode = hashCode * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(PanoramaLookupRadius);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowCurrentAddress);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowExitButton);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowHeadingCompass);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowProblemReporting);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ShowZoomButtons);
            return hashCode;
        }

        #endregion Properties
    }
}
