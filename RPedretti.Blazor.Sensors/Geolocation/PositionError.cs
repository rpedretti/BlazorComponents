using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Sensors.Geolocation
{
    public class PositionError
    {
        public ErrorCode Code { get; set; }
        public string Message { get; set; }
    }
}
