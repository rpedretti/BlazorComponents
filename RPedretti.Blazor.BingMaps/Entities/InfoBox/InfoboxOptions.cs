using System.Collections.Generic;

namespace RPedretti.Blazor.BingMaps.Entities.InfoBox
{
    public class InfoboxOptions
    {
        public InfoboxAction[] Actions { get; set; }
        public int? CloseDelayTime { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public Location Location { get; set; }
        public double? MaxHeight { get; set; }
        public double? MaxWidth { get; set; }
        public GeolocatonPoint Offset { get; set; }
        public bool? ShowCloseButton { get; set; }
        public bool? ShowPointer { get; set; }
        public string Title { get; set; }
        public bool? Visible { get; set; }
        public int? ZIndex { get; set; }

        public override bool Equals(object obj)
        {
            return obj is InfoboxOptions options &&
                   EqualityComparer<InfoboxAction[]>.Default.Equals(Actions, options.Actions) &&
                   CloseDelayTime == options.CloseDelayTime &&
                   Description == options.Description &&
                   HtmlContent == options.HtmlContent &&
                   EqualityComparer<Location>.Default.Equals(Location, options.Location) &&
                   MaxHeight == options.MaxHeight &&
                   MaxWidth == options.MaxWidth &&
                   EqualityComparer<GeolocatonPoint>.Default.Equals(Offset, options.Offset) &&
                   ShowCloseButton == options.ShowCloseButton &&
                   ShowPointer == options.ShowPointer &&
                   Title == options.Title &&
                   Visible == options.Visible &&
                   ZIndex == options.ZIndex;
        }

        public override int GetHashCode()
        {
            var hashCode = 459946268;
            hashCode = hashCode * -1521134295 + EqualityComparer<InfoboxAction[]>.Default.GetHashCode(Actions);
            hashCode = hashCode * -1521134295 + CloseDelayTime.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(HtmlContent);
            hashCode = hashCode * -1521134295 + EqualityComparer<Location>.Default.GetHashCode(Location);
            hashCode = hashCode * -1521134295 + MaxHeight.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxWidth.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<GeolocatonPoint>.Default.GetHashCode(Offset);
            hashCode = hashCode * -1521134295 + ShowCloseButton.GetHashCode();
            hashCode = hashCode * -1521134295 + ShowPointer.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + Visible.GetHashCode();
            hashCode = hashCode * -1521134295 + ZIndex.GetHashCode();
            return hashCode;
        }
    }
}