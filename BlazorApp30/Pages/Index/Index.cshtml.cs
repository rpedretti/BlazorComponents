using System.Drawing;
using BlazorApp30.Components;
using BlazorApp30.Components.Radio;

namespace BlazorApp30.Pages.Index
{
    public class IndexBase : BaseComponent
    {

        public Color ParentBgColor { get; set; }
        public Color ChildBgColor { get; set; }
        public string ParagraphStyle { get; set; }
        public bool ExpandLoaderAccordeon { get; set; }
        public RadioButton[] RadioButtons { get; set; } = new RadioButton[]
        {
            new RadioButton { Label = "Button 1", Value = 4 },
            new RadioButton { Label = "Button 2", Value = "olar"},
            new RadioButton { Label = "Button 3", Value = null, Disabled = true },
            new RadioButton { Label = "Button 4", Value = false }
        };

        private RadioButton _selectedRadioButton1;
        public RadioButton SelectedRadioButton1
        {
            get => _selectedRadioButton1;
            set => SetParameter(ref _selectedRadioButton1, value, StateHasChanged);
        }

        private RadioButton _selectedRadioButton2;
        public RadioButton SelectedRadioButton2
        {
            get => _selectedRadioButton2;
            set => SetParameter(ref _selectedRadioButton2, value, StateHasChanged);
        }

        private RadioButton _selectedRadioButton3;
        public RadioButton SelectedRadioButton3
        {
            get => _selectedRadioButton3;
            set => SetParameter(ref _selectedRadioButton3, value, StateHasChanged);
        }

        private bool _someChecked;
        protected bool SomeChecked
        {
            get => _someChecked;
            set => SetParameter(ref _someChecked, value, StateHasChanged);
        }

        private bool _someChecked2;
        protected bool SomeChecked2
        {
            get => _someChecked2;
            set => SetParameter(ref _someChecked2, value, StateHasChanged);
        }

        private bool _someToggled;
        protected bool SomeToggled
        {
            get => _someToggled;
            set => SetParameter(ref _someToggled, value, StateHasChanged);
        }

        private bool _someToggled2;
        protected bool SomeToggled2
        {
            get => _someToggled2;
            set => SetParameter(ref _someToggled2, value, StateHasChanged);
        }

        public IndexBase()
        {
            ParentBgColor = Color.AliceBlue;
            ChildBgColor = Color.FromArgb(220, 111, 25, 111);
            ParagraphStyle = "background: ";
        }

        protected void ResetSelectedRadios()
        {
            SelectedRadioButton1 = null;
            SelectedRadioButton2 = null;
            SelectedRadioButton3 = null;
        }

        protected bool HasSelection =>
            SelectedRadioButton1 != null ||
            SelectedRadioButton2 != null ||
            SelectedRadioButton3 != null;
    }
}
