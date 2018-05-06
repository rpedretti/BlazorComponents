using BlazorApp.Domain;
using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.NHContext
{
    public static class DataMock
    {
        public static async Task SeedData(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                if (!session.Query<WeatherForecast>().Any()) {
                    var json = File.ReadAllText("sample-data/weather.json");
                    var weathers = JsonConvert.DeserializeObject<List<WeatherForecast>>(json);
                    foreach (var weather in weathers)
                    {
                        await session.SaveAsync(weather);
                    }
                }
            }
        }
    }
}
