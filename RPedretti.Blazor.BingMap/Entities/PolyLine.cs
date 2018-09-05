using System.Collections.Generic;

namespace RPedretti.Blazor.BingMap.Entities
{
    public class PolyLine : BaseBingMapEntity
    {
        public List<Location> Locations { get; set; }

        public override void Dispose()
        {
        }
    }
}
