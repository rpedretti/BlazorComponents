using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using RPedretti.Blazor.Components;
using RPedretti.Blazor.Components.Radio;
using RPedretti.Blazor.Sensors.AmbientLight;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Index
{
    public class IndexBase : BaseComponent, IDisposable
    {
        #region Fields

        private readonly List<string> someList = new List<string>() {
            "olar 1", "olar 2", "banana", "apple", "bacalhau", "blabous", "bla", "abacate","abacate"
        };

        public int Light { get; set; }

        private bool _loadingSuggestions;

        private string _query;

        private RadioButton _selectedRadioButton1;

        private RadioButton _selectedRadioButton2;

        private RadioButton _selectedRadioButton3;

        private bool _someChecked;

        private bool _someChecked2;

        private bool _someToggled;

        private bool _someToggled2;

        #endregion Fields

        #region Properties

        protected List<string> FilteredList { get; set; }

        [Inject] protected AmbientLightSensor LightSensor { get; set; }

        protected bool HasSelection =>
            SelectedRadioButton1 != null ||
            SelectedRadioButton2 != null ||
            SelectedRadioButton3 != null;

        protected bool LoadingSuggestions
        {
            get => _loadingSuggestions;
            set => SetParameter(ref _loadingSuggestions, value);
        }

        protected bool SomeChecked
        {
            get => _someChecked;
            set => SetParameter(ref _someChecked, value, StateHasChanged);
        }

        protected bool SomeChecked2
        {
            get => _someChecked2;
            set => SetParameter(ref _someChecked2, value, StateHasChanged);
        }

        protected bool SomeToggled
        {
            get => _someToggled;
            set => SetParameter(ref _someToggled, value, StateHasChanged);
        }

        protected bool SomeToggled2
        {
            get => _someToggled2;
            set => SetParameter(ref _someToggled2, value, StateHasChanged);
        }

        #endregion Properties

        #region Methods

        protected override void OnInit()
        {
            LightSensor.OnReading += OnReading;
            LightSensor.OnError += OnError;
        }

        private void OnError(object sender, object e)
        {
            Console.WriteLine($"On error .NET: {e}");
        }

        private void OnReading(object sender, int reading)
        {
            Light = reading;
            StateHasChanged();
        }

        protected void DragDrop(UIDragEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Drop: " + JsonConvert.SerializeObject(args));
        }

        protected void DragEnd(UIDragEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Drag end: " + JsonConvert.SerializeObject(args));
        }

        protected void DragEnter(UIDragEventArgs args)
        {
            if (args.CtrlKey)
            {
                args.DataTransfer.DropEffect = "move";
            }
            else
            {
                args.DataTransfer.DropEffect = "copy";
            }

            System.Diagnostics.Debug.WriteLine("Drag enter: " + JsonConvert.SerializeObject(args));
        }

        protected void DragStart(UIDragEventArgs args)
        {
            args.DataTransfer.EffectAllowed = "copyMove";
            args.DataTransfer.Items = new UIDataTransferItem[]
            {
                new UIDataTransferItem { Type = "text/plain", Kind="olar" }
            };

            System.Diagnostics.Debug.WriteLine("drag start: " + JsonConvert.SerializeObject(args));
        }

        protected async Task FetchSuggestions(string query)
        {
            Query = query;
            if (!string.IsNullOrWhiteSpace(query))
            {
                LoadingSuggestions = true;
                StateHasChanged();
                await Task.Delay(1000);

                FilteredList = someList.Where(s => s.ToLower().StartsWith(query.ToLower())).ToList();
                LoadingSuggestions = false;
            }
            else
            {
                FilteredList = null;
            }
            StateHasChanged();
        }

        protected void ResetSelectedRadios()
        {
            SelectedRadioButton1 = null;
            SelectedRadioButton2 = null;
            SelectedRadioButton3 = null;
        }

        protected void SuggestionSelected(string suggestion)
        {
            FilteredList = null;
            Query = suggestion;
        }

        #endregion Methods

        #region Constructors

        public IndexBase()
        {
            ParentBgColor = Color.AliceBlue;
            ChildBgColor = Color.FromArgb(220, 111, 25, 111);
            ParagraphStyle = "background: ";
        }

        #endregion Constructors

        public Color ChildBgColor { get; set; }
        public bool ExpandLoaderAccordeon { get; set; }
        public string ParagraphStyle { get; set; }
        public Color ParentBgColor { get; set; }

        public string Query
        {
            get => _query;
            set => SetParameter(ref _query, value);
        }

        public RadioButton[] RadioButtons { get; set; } = new RadioButton[]
        {
            new RadioButton { Label = "Button 1", Value = 4 },
            new RadioButton { Label = "Button 2", Value = "olar"},
            new RadioButton { Label = "Button 3", Value = null, Disabled = true },
            new RadioButton { Label = "Button 4", Value = false }
        };

        public RadioButton SelectedRadioButton1
        {
            get => _selectedRadioButton1;
            set => SetParameter(ref _selectedRadioButton1, value);
        }

        public RadioButton SelectedRadioButton2
        {
            get => _selectedRadioButton2;
            set => SetParameter(ref _selectedRadioButton2, value);
        }

        public RadioButton SelectedRadioButton3
        {
            get => _selectedRadioButton3;
            set => SetParameter(ref _selectedRadioButton3, value);
        }

        public new void Dispose()
        {
            LightSensor.OnReading -= OnReading;
            LightSensor.OnError -= OnError;
        }
    }
}
