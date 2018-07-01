using System;

namespace RPedretti.Blazor.Components.DynamicTable
{
    public class DynamicTableCell
    {
        #region Fields

        private Func<string> _formatter;

        #endregion Fields

        #region Methods

        private string DefaultFormatter() => Content.ToString();

        #endregion Methods

        #region Properties

        public string Classes { get; set; }
        public object Content { get; set; }

        public Func<string> Formatter
        {
            get => _formatter ?? DefaultFormatter;
            set => _formatter = value;
        }

        #endregion Properties
    }
}
