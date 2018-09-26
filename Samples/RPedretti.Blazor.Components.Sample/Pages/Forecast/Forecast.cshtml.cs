using Microsoft.AspNetCore.Blazor.Components;
using RPedretti.Blazor.Components.Layout.DynamicTable;
using RPedretti.Blazor.Components.Sample.Domain;
using RPedretti.Blazor.Components.Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.Components.Sample.Pages
{
    public class ForecastBase : BaseComponent
    {
        #region Fields

        private bool _grouped;

        private bool _loading;

        #endregion Fields

        #region Properties

        [Inject]
        private IForecastService ForecastService { get; set; }
        protected List<DynamicTableColumn<WeatherForecast>> Columns { get; set; }

        #endregion Properties

        #region Methods

        private async Task GetForecastAsync()
        {
            Loading = true;

            var forecasts = await ForecastService.GetForecastAsync();

            Forecasts = forecasts.Select(f => new DynamicTableRow<WeatherForecast> { Context = f }).ToList();

            GroupedForecast = forecasts.GroupBy(f => f.Date.Date).Select(g =>
            {
                var group = new DynamicTableGroup<WeatherForecast>
                {
                    Rows = g.Select(f => new DynamicTableRow<WeatherForecast> { Context = f }).ToList()
                };

                return group;
            }).ToList();

            Loading = false;
        }

        #endregion Methods

        protected List<DynamicTableRow<WeatherForecast>> Forecasts { get; set; }

        protected List<DynamicTableGroup<WeatherForecast>> GroupedForecast { get; set; }

        protected List<DynamicTableHeader> Headers { get; set; }

        protected bool Loading
        {
            get => _loading;
            set => SetParameter(ref _loading, value, StateHasChanged);
        }

        protected override async Task OnInitAsync()
        {
            await GetForecastAsync();
            StateHasChanged();
        }

        public bool Grouped
        {
            get => _grouped;
            set => SetParameter(ref _grouped, value, StateHasChanged);
        }

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

        protected async Task Sort(string sortId, bool isAsc)
        {
            Loading = true;

            await Task.Delay(1500);

            var column = Columns.ElementAt(int.Parse(sortId));

            if (isAsc)
            {
                switch (column.SortProp)
                {
                    case nameof(WeatherForecast.Date):
                        Forecasts = Forecasts.OrderBy(r => r.Context.Date).ToList();
                        break;
                    case nameof(WeatherForecast.RainAmmount):
                        Forecasts = Forecasts.OrderBy(r => r.Context.RainAmmount).ToList();
                        break;
                    case nameof(WeatherForecast.RainChangePercent):
                        Forecasts = Forecasts.OrderBy(r => r.Context.RainChangePercent).ToList();
                        break;
                    case nameof(WeatherForecast.Temperature):
                        Forecasts = Forecasts.OrderBy(r => r.Context.Temperature).ToList();
                        break;
                }
            }
            else
            {
                switch (column.SortProp)
                {
                    case nameof(WeatherForecast.Date):
                        Forecasts = Forecasts.OrderByDescending(r => r.Context.Date).ToList();
                        break;
                    case nameof(WeatherForecast.RainAmmount):
                        Forecasts = Forecasts.OrderByDescending(r => r.Context.RainAmmount).ToList();
                        break;
                    case nameof(WeatherForecast.RainChangePercent):
                        Forecasts = Forecasts.OrderByDescending(r => r.Context.RainChangePercent).ToList();
                        break;
                    case nameof(WeatherForecast.Temperature):
                        Forecasts = Forecasts.OrderByDescending(r => r.Context.Temperature).ToList();
                        break;
                }
            }

            Loading = false;
        }
    }
}
