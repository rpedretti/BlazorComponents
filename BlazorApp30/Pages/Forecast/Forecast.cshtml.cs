using BlazorApp30.Components.DynamicTable;
using BlazorApp30.Services;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Pages
{
    public class ForecastBase : BaseBlazorPage
    {
        [Inject]
        private IForecastService ForecastService { get; set; }

        protected bool Loading { get; set; }
        protected List<DynamicTableHeader> Headers { get; set; }
        protected List<DynamicTableRow> Forecasts { get; set; }
        protected List<DynamicTableGroup> GroupedForecast { get; set; }
        private bool _grouped;

        public bool Grouped
        {
            get { return _grouped; }
            set { SetParameter(ref _grouped, value); }
        }

        protected override async Task OnInitAsync()
        {
            await GetForecastAsync();
            StateHasChanged();
        }

        private async Task GetForecastAsync()
        {
            Loading = true;
            Headers = new List<DynamicTableHeader>()
            {
                new DynamicTableHeader{ Title =  "Date", Classes = "-l"},
                new DynamicTableHeader{ Title =  "Temp. (C)", CanSort = true, Classes = "-l" },
                new DynamicTableHeader{ Title =  "Rain chance (%)", CanSort = true, Classes = "-r" },
                new DynamicTableHeader{ Title =  "Rain Ammount (mm)", CanSort = true, Classes = "-r" }
            };

            var forecasts = await ForecastService.GetForecastAsync();

            Forecasts = forecasts.Select(f =>
             {
                 var props = new List<DynamicTableCell>
                {
                    new DynamicTableCell { Content = f.Date, Classes = "-l" },
                    new DynamicTableCell { Content = f.Temperature, Classes = "-l" },
                    new DynamicTableCell { Content = f.RainChangePercent, Classes = "-r" },
                    new DynamicTableCell { Content = f.RainAmmount, Classes = "-r" }
                };

                 return new DynamicTableRow { Cells = props };
             }).ToList();

            GroupedForecast = forecasts.GroupBy(f => f.Date.Date).Select(g =>
            {
                var group = new DynamicTableGroup
                {
                    Title = $"{g.Key.ToShortDateString()} -> Avg temp (C): {Math.Round(g.Average(f => f.Temperature), 1, MidpointRounding.ToEven)}",
                    Rows = g.Select(f =>
                    {
                        var props = new List<DynamicTableCell>
                        {
                            new DynamicTableCell { Content = f.Date, Classes = "forecast-date -l" },
                            new DynamicTableCell { Content = f.Temperature, Classes = "-l" },
                            new DynamicTableCell { Content = f.RainChangePercent, Classes = "-r" },
                            new DynamicTableCell { Content = f.RainAmmount, Classes = "-r" }
                        };

                        return new DynamicTableRow { Cells = props };
                    }).ToList()
                };

                return group;
            }).ToList();


            Loading = false;
        }

        public void ToggleColumn(int index)
        {
            Headers.ElementAt(index).Hidden = !Headers.ElementAt(index).Hidden;
        }
    }
}
