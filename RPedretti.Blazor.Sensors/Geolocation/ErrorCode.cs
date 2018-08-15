using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Sensors.Geolocation
{
    public enum ErrorCode
    {
        PermissionDenied = 1,
        PositionUnavailable = 2,
        Timeout = 3
    }
}
