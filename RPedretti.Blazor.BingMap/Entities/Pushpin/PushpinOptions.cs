using System;
using System.Collections.Generic;
using System.Drawing;

namespace RPedretti.Blazor.BingMap.Entities.Pushpin
{
    public class PushpinOptions : ICloneable
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
        public string Text { get; set; }
        public Point? TextOffset { get; set; }
        public string Titlte { get; set; }
        public bool? Visible { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            var options = obj as PushpinOptions;
            return options != null &&
                   EqualityComparer<Point?>.Default.Equals(Anchor, options.Anchor) &&
                   EqualityComparer<Color?>.Default.Equals(Color, options.Color) &&
                   Cursor == options.Cursor &&
                   EqualityComparer<bool?>.Default.Equals(Draggable, options.Draggable) &&
                   EqualityComparer<bool?>.Default.Equals(EnableClickedStyle, options.EnableClickedStyle) &&
                   EqualityComparer<bool?>.Default.Equals(EnableHoverStyle, options.EnableHoverStyle) &&
                   Icon == options.Icon &&
                   EqualityComparer<bool?>.Default.Equals(RoundClickableArea, options.RoundClickableArea) &&
                   SubTitlte == options.SubTitlte &&
                   Text == options.Text &&
                   EqualityComparer<Point?>.Default.Equals(TextOffset, options.TextOffset) &&
                   Titlte == options.Titlte &&
                   EqualityComparer<bool?>.Default.Equals(Visible, options.Visible);
        }

        public override int GetHashCode()
        {
            var hashCode = 1655777802;
            hashCode = hashCode * -1521134295 + EqualityComparer<Point?>.Default.GetHashCode(Anchor);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color?>.Default.GetHashCode(Color);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Cursor);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(Draggable);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(EnableClickedStyle);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(EnableHoverStyle);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Icon);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(RoundClickableArea);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SubTitlte);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            hashCode = hashCode * -1521134295 + EqualityComparer<Point?>.Default.GetHashCode(TextOffset);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Titlte);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(Visible);
            return hashCode;
        }

        #endregion Properties
    }
}
