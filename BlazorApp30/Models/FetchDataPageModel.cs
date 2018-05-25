using BlazorApp30.Components.DynamicTable;
using BlazorApp30.Domain;
using System.Collections.Generic;

namespace BlazorApp30.Models
{
    public class FetchDataPageModel
    {
        public DynamicTableHeader[] Headers { get; set; }
        public IEnumerable<DynamicTableRow> Forecasts { get; set; }
    }
}
