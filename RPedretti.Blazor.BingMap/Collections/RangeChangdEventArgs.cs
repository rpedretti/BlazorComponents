namespace RPedretti.Blazor.BingMap.Collections
{
    public class RangeChangdEventArgs
    {
        public RangeChangdEventArgs(int startIndex, int ammount, RangeChangeType type)
        {
            StartIndex = startIndex;
            Ammount = ammount;
            Type = type;
        }

        public RangeChangeType Type { get; set; }
        public int StartIndex { get; set; }
        public int Ammount { get; set; }
    }
}