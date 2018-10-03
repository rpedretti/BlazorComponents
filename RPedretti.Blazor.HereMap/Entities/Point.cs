namespace RPedretti.Blazor.HereMap.Entities
{
    public class Point
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public AltitudeContext AltitudeContext { get; set; }
    }
}