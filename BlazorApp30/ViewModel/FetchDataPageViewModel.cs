using BlazorApp30.Components.DynamicTable;
using BlazorApp30.Domain;
using BlazorApp30.Models;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace BlazorApp30.ViewModel
{
    public class FetchDataPageViewModel
    {
        private readonly HttpClient _httpClient;
        public FetchDataPageModel Model { get; set; } = new FetchDataPageModel();

        public bool Loading { get; private set; }

        public FetchDataPageViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task FetchDataAsync()
        {
            Loading = true;
            Model.Headers = new DynamicTableHeader[]
            {
                new DynamicTableHeader{ Title =  "Date", Classes = "forecast-date -l"},
                new DynamicTableHeader{ Title =  "Temp. (C)", CanSort = true, Classes = "-l" },
                new DynamicTableHeader{ Title =  "Temp. (F)", CanSort = true, Classes = "-r" },
                new DynamicTableHeader{ Title =  "Summary", CanSort = true, Classes = "-r" }
            };

            var forecasts = await _httpClient.GetJsonAsync<WeatherForecast[]>("/sample-data/weather.json");
            Model.Forecasts = forecasts.Select(f =>
            {
                var props = new DynamicTableCell[]
                {
                    new DynamicTableCell { Content = f.Date, Classes = "forecast-date -l", Formatter = f.Date.ToShortDateString },
                    new DynamicTableCell { Content = f.TemperatureC, Classes = "-l"  },
                    new DynamicTableCell { Content = f.TemperatureF, Classes = "-r"  },
                    new DynamicTableCell { Content = f.Summary, Classes = "-r"  }
                };

                return new DynamicTableRow { Cells = props, Classes = "alternate" };
            });

            Loading = false;
        }

        public void ToggleColumn(int index)
        {
            Model.Headers.ElementAt(index).Hidden = !Model.Headers.ElementAt(index).Hidden;
        }
    }
}
