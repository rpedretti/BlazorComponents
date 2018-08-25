namespace RPedretti.Blazor.BingMaps.Entities
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

        #endregion Properties
    }
}
