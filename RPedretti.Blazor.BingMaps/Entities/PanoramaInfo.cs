using System.Collections.Generic;

namespace RPedretti.Blazor.BingMaps.Entities
{
    public class PanoramaInfo
    {
        #region Properties

        public string Cd { get; set; }

        public override bool Equals(object obj)
        {
            var info = obj as PanoramaInfo;
            return info != null &&
                   Cd == info.Cd;
        }

        public override int GetHashCode()
        {
            return -1896984974 + EqualityComparer<string>.Default.GetHashCode(Cd);
        }

        #endregion Properties
    }
}
