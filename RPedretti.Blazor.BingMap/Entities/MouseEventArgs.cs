using RPedretti.Blazor.BingMap.Entities.Layer;

namespace RPedretti.Blazor.BingMap.Entities
{
    public class MouseEventArgs<T>
    {
        public string EventName { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSecondary { get; set; }
        public BingMapLayer Layer { get; set; }
        public Geocoordinate Location { get; set; }
        public double PageX { get; set; }
        public double PageY { get; set; }
        public GeolocatonPoint Point { get; set; }
        public T Target { get; set; }
        public string TargetType { get; set; }
        public double WheelDelta { get; set; }
    }
}
