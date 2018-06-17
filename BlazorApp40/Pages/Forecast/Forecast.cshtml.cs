using BlazorApp40.Components;
using BlazorApp40.Services;
using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components;
using RPedretti.Blazor.Components.DynamicTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp40.Pages
{
    public class ForecastBase : BaseComponent
    {
        [Inject]
        private IForecastService ForecastService { get; set; }

        private bool _loading;
        protected bool Loading
        {
            get => _loading;
            set => SetParameter(ref _loading, value, StateHasChanged);
        }

        private bool _grouped;
        public bool Grouped
        {
            get => _grouped;
            set => SetParameter(ref _grouped, value, StateHasChanged);
        }

        protected List<DynamicTableHeader> Headers { get; set; }
        protected List<DynamicTableRow> Forecasts { get; set; }
        protected List<DynamicTableGroup> GroupedForecast { get; set; }

        public ForecastBase()
        {
            Headers = new List<DynamicTableHeader>()
            {
                new DynamicTableHeader{ Title =  "Date", Classes = "-l"},
                new DynamicTableHeader{ Title =  "Temp. (C)", CanSort = true, Classes = "-l", SortId = "1" },
                new DynamicTableHeader{ Title =  "Rain chance (%)", CanSort = true, Classes = "-r", SortId = "2" },
                new DynamicTableHeader{ Title =  "Rain Ammount (mm)", CanSort = true, Classes = "-r", SortId = "3" }
            };
        }

        public void ToggleColumn(int index)
        {
            Headers.ElementAt(index).Hidden = !Headers.ElementAt(index).Hidden;
        }

        protected override async Task OnInitAsync()
        {
            await GetForecastAsync();
            StateHasChanged();
        }

        protected async Task Sort(string sortId, bool isAsc)
        {
            Loading = true;

            await Task.Delay(1500);

            if (isAsc)
            {
                Forecasts = Forecasts.OrderBy(r => r.Cells.ElementAt(int.Parse(sortId)).Content).ToList();
            }
            else
            {
                Forecasts = Forecasts.OrderByDescending(r => r.Cells.ElementAt(int.Parse(sortId)).Content).ToList();
            }

            Loading = false;
        }

        private async Task GetForecastAsync()
        {
            Loading = true;

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
    }
}
