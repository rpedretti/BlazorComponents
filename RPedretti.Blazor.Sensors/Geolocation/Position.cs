using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Sensors.Geolocation
{
    public class Position
    {
        public Coordinates Coords { get; set; }
        public long Timestamp { get; set; }
    }
}
