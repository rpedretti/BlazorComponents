using System.Drawing;

namespace BlazorApp30.Extensions
{
    public static class ColorConverterExtensions
    {
        public static string ToHexString(this Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}{c.A:X2}";
        public static string ToRgbString(this Color c) => $"RGB({c.R}, {c.G}, {c.B})";
        public static string ToRgbaString(this Color c) => $"rgba({c.R}, {c.G}, {c.B},{c.A})";
    }
}
