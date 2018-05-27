using BlazorApp30.Components.DynamicTable;
using BlazorApp30.Domain;
using System.Collections.Generic;

namespace BlazorApp30.Models
{
    public class FetchDataPageModel
    {
        public List<DynamicTableHeader> Headers { get; set; }
        public List<DynamicTableRow> Forecasts { get; set; }
        public List<DynamicTableGroup> GroupedForecast { get; set; }
    }
}
