using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Entities.Polyline
{
    public partial class BingMapPolyline : BaseBingMapEntity
    {
        private const string _polylineNamespace = "rpedrettiBlazorComponents.bingMaps.polyline";
        private const string _polylineGet = _polylineNamespace + ".getPropertie";
        private const string clearEventsFunctionName = _polylineNamespace + ".clearEvents";

        private DotNetObjectRef thisRef;
        private BindingList<Location> _coordinates = new BindingList<Location>();
        private BingMapPolylineOptions _options;

        public BindingList<Location> Coordinates
        {
            get => _coordinates;
            set
            {
                if (!EqualityComparer<BindingList<Location>>.Default.Equals(_coordinates, value))
                {
                    if (_coordinates != null)
                    {
                        _coordinates.ListChanged -= CoordinatesChanged;
                    }

                    _coordinates = value;

                    if (_coordinates != null)
                    {
                        _coordinates.ListChanged += CoordinatesChanged;
                    }
                }
            }
        }

        private void CoordinatesChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            RaisePropertyChanged();
        }

        public BingMapPolylineOptions OptionsSnapshot { get; set; } = new BingMapPolylineOptions
        {
            Cursor = "pointer",
            Generalizable = true,
            Visible = true
        };
        public BingMapPolylineOptions Options
        {
            get => _options;
            set => SetParameter(ref _options, value, () => MergeSnapshot(value));
        }

        private void MergeSnapshot(BingMapPolylineOptions value)
        {
            OptionsSnapshot.Cursor = value?.Cursor ?? OptionsSnapshot?.Cursor;
            OptionsSnapshot.Generalizable = value?.Generalizable ?? OptionsSnapshot?.Generalizable;
            OptionsSnapshot.StrokeColor = value?.StrokeColor ?? OptionsSnapshot?.StrokeColor;
            OptionsSnapshot.StrokeDashArray = value?.StrokeDashArray ?? OptionsSnapshot?.StrokeDashArray;
            OptionsSnapshot.StrokeThickness = value?.StrokeThickness ?? OptionsSnapshot?.StrokeThickness;
            OptionsSnapshot.Visible = value?.Visible ?? OptionsSnapshot?.Visible;
        }

        public string Metadata { get; set; }

        public BingMapPolyline()
        {
            thisRef = new DotNetObjectRef(this);
            Id = Id ?? "polyline-" + Guid.NewGuid().ToString();
        }

        public override async void Dispose()
        {
            try
            {
                await JSRuntime.Current.InvokeAsync<object>($"{_polylineNamespace}.remove", Id);
                JSRuntime.Current.UntrackObjectRef(thisRef);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CheckThisRef()
        {
            if (_onClick == null && _onDoubleClick == null && _onMouseDown == null && _onMouseOut == null && _onMouseOver == null && _onMouseUp == null)
            {
                if (thisRef != null)
                {
                    JSRuntime.Current.UntrackObjectRef(thisRef);
                }
            }
        }

        private void AssureThisRef()
        {
            if (thisRef == null)
            {
                thisRef = new DotNetObjectRef(this);
            }
        }
    }
}
