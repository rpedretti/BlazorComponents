using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp40.Components;
using Microsoft.AspNetCore.Blazor;
using RPedretti.Blazor.Components;
using RPedretti.Blazor.Components.Radio;

namespace BlazorApp40.Pages.Index
{
    public class IndexBase : BaseComponent
    {

        public Color ParentBgColor { get; set; }
        public Color ChildBgColor { get; set; }
        public string ParagraphStyle { get; set; }
        public bool ExpandLoaderAccordeon { get; set; }

        private string _query;
        public string Query
        {
            get { return _query; }
            set { SetParameter(ref _query, value, StateHasChanged); }
        }

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

        protected List<string> FilteredList { get; set; }

        private readonly List<string> someList = new List<string>() {
            "olar 1", "olar 2", "banana", "apple", "bacalhau", "blabous", "bla", "abacate","abacate"
        };

        private bool _loadingSuggestions;
        protected bool LoadingSuggestions
        {
            get => _loadingSuggestions;
            set => SetParameter(ref _loadingSuggestions, value, StateHasChanged);
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

        protected void DragStart(UIDragEventArgs args)
        {
            args.DataTransfer.EffectAllowed = "copyMove";
            args.DataTransfer.Items = new UIDataTransferItem[]
            {
                new UIDataTransferItem { Type = "text/plain", Kind="olar" }
            };

            System.Diagnostics.Debug.WriteLine("drag start: " + JsonUtil.Serialize(args));
        }

        protected void DragEnd(UIDragEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Drag end: " + JsonUtil.Serialize(args));
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

            System.Diagnostics.Debug.WriteLine("Drag enter: " + JsonUtil.Serialize(args));
        }

        protected void DragDrop(UIDragEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Drop: " + JsonUtil.Serialize(args));
        }

        protected async Task FetchSuggestions(string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                LoadingSuggestions = true;

                await Task.Delay(1000);

                FilteredList = someList.Where(s => s.ToLower().StartsWith(query.ToLower())).ToList();
                LoadingSuggestions = false;
            }
            else
            {
                FilteredList = null;
                StateHasChanged();
            }
        }

        protected void SuggestionSelected(string suggestion)
        {
            FilteredList = null;
            Query = suggestion;
        }

        protected bool HasSelection =>
            SelectedRadioButton1 != null ||
            SelectedRadioButton2 != null ||
            SelectedRadioButton3 != null;
    }
}
