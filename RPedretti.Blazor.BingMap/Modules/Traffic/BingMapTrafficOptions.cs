namespace RPedretti.Blazor.BingMap.Modules.Traffic
{
    public class BingMapTrafficOptions
    {
        #region Properties

        public bool? FlowVisible { get; set; }
        public bool? IncidentsVisible { get; set; }
        public bool? LegendVisible { get; set; }
        public double Opacity { get; set; } = 1;

        #endregion Properties
    }
}
