using System;

namespace BlazorApp40.Domain
{
    public class WeatherForecast
    {
        #region Properties

        public DateTime Date { get; set; }
        public int RainAmmount { get; set; }
        public int RainChangePercent { get; set; }
        public int Temperature { get; set; }

        #endregion Properties
    }
}
