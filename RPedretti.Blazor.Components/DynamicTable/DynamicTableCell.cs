using System;

namespace RPedretti.Blazor.Components.DynamicTable
{
    public class DynamicTableCell
    {
        public object Content { get; set; }
        public string Classes { get; set; }

        private Func<string> _formatter;
        public Func<string> Formatter
        {
            get => _formatter ?? DefaultFormatter;
            set => _formatter = value;
        }

        private string DefaultFormatter() => Content.ToString();
    }
}
