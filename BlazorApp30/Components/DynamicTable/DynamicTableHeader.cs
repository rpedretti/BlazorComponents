using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Components.DynamicTable
{
    public class DynamicTableHeader
    {
        public string Title { get; set; }
        public bool CanSort { get; set; }
        public bool Hidden { get; set; }
        public string Classes { get; set; }
    }
}
