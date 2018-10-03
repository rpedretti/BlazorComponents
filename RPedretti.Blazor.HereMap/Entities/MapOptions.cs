using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.HereMap.Entities
{
    public class MapOptions
    {
        public Point Center { get; set; }
        public int? Zoom { get; set; }
        public Rect Bounds { get; set; }
        // public IEnumerable<Layer> Layers { get; set; }
        public EngineType? EngineType { get; set; }
        public int? PixelRatio { get; set; }
        public ImprintOptions Imprint { get; set; }
        public BackgroundRange RenderBaseBackground { get; set; }
        public bool? AutoColor { get; set; }
        public int? Margin { get; set; }
        public Padding Padding { get; set; }
        public bool? FixedCenter { get; set; }
    }
}
