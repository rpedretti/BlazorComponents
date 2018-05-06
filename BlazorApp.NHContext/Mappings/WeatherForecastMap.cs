using BlazorApp.Domain;
using FluentNHibernate.Mapping;

namespace BlazorApp.NHContext.Mappings
{
    public class WeatherForecastMap : ClassMap<WeatherForecast>
    {
        public WeatherForecastMap()
        {
            Id(w => w.Id);
            Map(w => w.Date);
            Map(w => w.Summary);
            Map(w => w.TemperatureC);
            Map(w => w.TemperatureF);
        }
    }
}
