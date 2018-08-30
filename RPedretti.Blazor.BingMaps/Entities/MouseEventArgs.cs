using RPedretti.Blazor.BingMaps.Entities.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps.Entities
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
