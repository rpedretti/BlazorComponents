namespace RPedretti.Blazor.Components.BingMaps.Entities
{
    public class Geocoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double AltitudeReference { get; set; } = -1;

        public override bool Equals(object obj)
        {
            return obj is Geocoordinate geocoordinate &&
                   Latitude == geocoordinate.Latitude &&
                   Longitude == geocoordinate.Longitude &&
                   Altitude == geocoordinate.Altitude &&
                   AltitudeReference == geocoordinate.AltitudeReference;
        }

        public override int GetHashCode()
        {
            var hashCode = -1575447554;
            hashCode = hashCode * -1521134295 + Latitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Longitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Altitude.GetHashCode();
            hashCode = hashCode * -1521134295 + AltitudeReference.GetHashCode();
            return hashCode;
        }
    }
}
