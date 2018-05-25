using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp30.Components.DynamicTable
{
    public class DynamicTableCell
    {
        public object Content { get; set; }
        public string Classes { get; set; }

        private Func<string> _formatter;
        public Func<string> Formatter
        {
            get { return _formatter ?? DefaultFormatter; }
            set { _formatter = value; }
        }

        private string DefaultFormatter() => Content.ToString();
    }
}
