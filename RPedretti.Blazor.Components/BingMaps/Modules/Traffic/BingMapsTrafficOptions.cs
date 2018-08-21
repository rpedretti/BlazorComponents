using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.BingMaps.Modules.Traffic
{
    public class BingMapsTrafficOptions
    {
        public bool? FlowVisible { get; set; }
        public bool? IncidentsVisible { get; set; }
        public bool? LegendVisible { get; set; }
        public double Opacity { get; set; } = 1;
    }
}
