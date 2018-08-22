using System.Drawing;

namespace RPedretti.Blazor.BingMaps.Entities
{
    public class PushpinOptions
    {
        #region Properties

        public Point? Anchor { get; set; }
        public Color? Color { get; set; }
        public string Cursor { get; set; }
        public bool? Draggable { get; set; }
        public bool? EnableClickedStyle { get; set; }
        public bool? EnableHoverStyle { get; set; }
        public string Icon { get; set; }
        public bool? RoundClickableArea { get; set; }
        public string SubTitlte { get; set; }
        public string Titlte { get; set; }
        public string Text { get; set; }
        public Point? TextOffset { get; set; }
        public bool? Visible { get; set; }

        #endregion Properties
    }
}
