using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.BingMaps.Entities
{
    public class BingMapPushpin
    {
        public string Id { get; set; }
        public Geocoordinate Center { get; set; }
        public PushpinOptions Options { get; set; }
    }
}
