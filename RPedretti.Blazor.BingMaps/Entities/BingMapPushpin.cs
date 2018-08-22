namespace RPedretti.Blazor.BingMaps.Entities
{
    public class BingMapPushpin
    {
        public string Id { get; set; }
        public Geocoordinate Center { get; set; }
        public PushpinOptions Options { get; set; }
    }
}
