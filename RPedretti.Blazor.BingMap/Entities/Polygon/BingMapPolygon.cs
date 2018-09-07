using Microsoft.JSInterop;
using RPedretti.Blazor.Shared.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Entities.Polygon
{
    public partial class BingMapPolygon : BaseBingMapEntity
    {
        private const string _polygonNamespace = "rpedrettiBlazorComponents.bingMaps.polygon";
        private const string _polygonGet = _polygonNamespace + ".getPropertie";
        private const string clearEventsFunctionName = _polygonNamespace + ".clearEvents";

        private DotNetObjectRef thisRef;
        private BindingList<Geocoordinate> _coordinates = new BindingList<Geocoordinate>();
        private BindingList<Geocoordinate[]> _rings = new BindingList<Geocoordinate[]>();
        private BingMapPolygonOptions _options;

        public BindingList<Geocoordinate> Coordinates
        {
            get => _coordinates;
            set
            {
                if (!EqualityComparer<BindingList<Geocoordinate>>.Default.Equals(_coordinates, value))
                {
                    if (_coordinates != null)
                    {
                        _coordinates.ListChanged -= CoordinatesChanged;
                    }

                    SetParameter(ref _coordinates, value);

                    if (_coordinates != null)
                    {
                        _coordinates.ListChanged += CoordinatesChanged;
                    }
                }
            }
        }

        public BindingList<Geocoordinate[]> Rings
        {
            get => _rings;
            set
            {
                if (!EqualityComparer<BindingList<Geocoordinate[]>>.Default.Equals(_rings, value))
                {
                    if (_rings != null)
                    {
                        _rings.ListChanged -= RingsChanged;
                    }

                    SetParameter(ref _rings, value);

                    if (_rings != null)
                    {
                        _rings.ListChanged += RingsChanged;
                    }
                }
            }
        }

        private void RingsChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            RaisePropertyChanged();
        }

        private void CoordinatesChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            RaisePropertyChanged();
        }

        private async Task UpdateLocations()
        {
            try
            {
                await JSRuntime.Current.InvokeAsync<object>($"{_polygonNamespace}.update", Id, this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public BingMapPolygonOptions OptionsSnapshot { get; set; } = new BingMapPolygonOptions
        {
            Cursor = "pointer",
            Generalizable = true,
            Visible = true
        };
        public BingMapPolygonOptions Options
        {
            get => _options;
            set => SetParameter(ref _options, value, () => MergeSnapshot(value));
        }

        private void MergeSnapshot(BingMapPolygonOptions value)
        {
            OptionsSnapshot.Cursor = value?.Cursor ?? OptionsSnapshot?.Cursor;
            OptionsSnapshot.Generalizable = value?.Generalizable ?? OptionsSnapshot?.Generalizable;
            OptionsSnapshot.StrokeColor = value?.StrokeColor ?? OptionsSnapshot?.StrokeColor;
            OptionsSnapshot.FillColor = value?.FillColor ?? OptionsSnapshot?.FillColor;
            OptionsSnapshot.StrokeDashArray = value?.StrokeDashArray ?? OptionsSnapshot?.StrokeDashArray;
            OptionsSnapshot.StrokeThickness = value?.StrokeThickness ?? OptionsSnapshot?.StrokeThickness;
            OptionsSnapshot.Visible = value?.Visible ?? OptionsSnapshot?.Visible;
        }

        public string Metadata { get; set; }

        public BingMapPolygon()
        {
            thisRef = new DotNetObjectRef(this);
            Id = Id ?? "polygon-" + Guid.NewGuid().ToString();
        }

        public override async void Dispose()
        {
            try
            {
                await JSRuntime.Current.InvokeAsync<object>($"{_polygonNamespace}.remove", Id);
                JSRuntime.Current.UntrackObjectRef(thisRef);
            }
            catch (Exception e)
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
