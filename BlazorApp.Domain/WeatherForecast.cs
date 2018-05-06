using System;

namespace BlazorApp.Domain
{
    public class WeatherForecast
    {
        public virtual long Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int TemperatureC { get; set; }
        public virtual int TemperatureF { get; set; }
        public virtual string Summary { get; set; }
    }
}
