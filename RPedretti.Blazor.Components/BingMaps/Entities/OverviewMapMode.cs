using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.BingMaps.Entities
{
    public class OverviewMapMode
    {
        public byte Minimized => 0;
        public byte Expanded => 1;
        public byte Hidden => 2;
    }
}
