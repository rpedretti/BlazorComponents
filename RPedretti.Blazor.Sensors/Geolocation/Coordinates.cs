namespace RPedretti.Blazor.Sensors.Geolocation
{
    public class Coordinates
    {
        #region Properties

        public double Accuracy { get; set; }
        public double? Altitude { get; set; }
        public double? AltitudeAccuracy { get; set; }
        public double? Heading { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Speed { get; set; }

        #endregion Properties
    }
}
