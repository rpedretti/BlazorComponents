using RPedretti.Blazor.BingMap.Entities;
using RPedretti.Blazor.BingMap.Entities.Polyline;
using RPedretti.Blazor.Shared.Operators;
using System;
using System.Threading.Tasks;
using System.Threading;
using RPedretti.Blazor.BingMap.Collections;
using System.ComponentModel;

namespace RPedretti.Blazor.BingMap.Sample.Pages.PolyLinePage
{
    public class PolyLinePageBase : BaseComponent, IDisposable
    {
        protected string BingMapId = $"bing-map-{Guid.NewGuid().ToString().Replace("-", "")}";
        protected BingMapEntityList Entities = new BingMapEntityList();
        protected DebounceDispatcher clickDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher doubleClickDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher downDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher upDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher outDispatcher = new DebounceDispatcher();
        private BingMapPolyline polyLine;
        private Timer changePolylineTimer;

        public bool Loading { get; set; } = true;

        protected BingMapConfig MapConfig { get; set; } = new BingMapConfig
        {
            MapTypeId = BingMapTypes.Road,
            SupportedMapTypes = new string[] {
                BingMapTypes.Aerial,
                BingMapTypes.GrayScale,
                BingMapTypes.Road,
                BingMapTypes.BirdsEyes
            },
            Center = new Geocoordinate { Latitude = 0, Longitude = 0 },
            EnableHighDpi = true,
            Zoom = 2,
            ShowTrafficButton = true
        };

        protected BingMapsViewConfig MapViewConfig { get; set; } = new BingMapsViewConfig();
        public bool MouseUp { get; set; }
        public bool MouseOver { get; set; }
        public bool MouseOut { get; set; }
        public bool MouseDown { get; set; }
        public bool DoubleClick { get; set; }
        public bool Click { get; set; }

        protected Task MapLoaded()
        {
            Loading = false;
            Task.Run(StateHasChanged);

            polyLine = new BingMapPolyline
            {
                Coordinates = new BindingList<Location>
                {
                    new Location(10, 10),
                    new Location(0, 0),
                    new Location(10, -10),
                },
                Options = new BingMapPolylineOptions
                {
                    StrokeThickness = 3,
                    StrokeDashArray = new int[] { 3, 3, 0, 2 }
                }
            };

            polyLine.OnClick += PolyLine_OnClick;
            polyLine.OnDoubleClick += PolyLine_OnDoubleClick;
            polyLine.OnMouseDown += PolyLine_OnMouseDown;
            polyLine.OnMouseOut += PolyLine_OnMouseOut;
            polyLine.OnMouseOver += PolyLine_OnMouseOver;
            polyLine.OnMouseUp += PolyLine_OnMouseUp;

            Entities.Add(polyLine);

            changePolylineTimer = new Timer((o) =>
            {
                polyLine.Coordinates.Insert(1, new Location(4, 12));
                StateHasChanged();
            }, null, 3000, Timeout.Infinite);

            return Task.CompletedTask;
        }

        private void PolyLine_OnMouseUp(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            MouseUp = true;
            upDispatcher.Debounce(2000, (o) => { MouseUp = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void PolyLine_OnMouseOver(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            MouseOver = true;
            MouseOut = false;
            StateHasChanged();
        }

        private void PolyLine_OnMouseOut(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            MouseOver = false;
            MouseOut = true;
            outDispatcher.Debounce(2000, (o) => { MouseOut = false; StateHasChanged(); });

            StateHasChanged();
        }

        private void PolyLine_OnMouseDown(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            MouseDown = true;
            downDispatcher.Debounce(2000, (o) => { MouseDown = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void PolyLine_OnDoubleClick(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            DoubleClick = true;
            doubleClickDispatcher.Debounce(2000, (o) => { DoubleClick = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void PolyLine_OnClick(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            Click = true;
            clickDispatcher.Debounce(2000, (o) => { Click = false; StateHasChanged(); });
            StateHasChanged();
        }

        public void Dispose()
        {
            polyLine.OnClick -= PolyLine_OnClick;
            polyLine.OnDoubleClick -= PolyLine_OnDoubleClick;
            polyLine.OnMouseDown -= PolyLine_OnMouseDown;
            polyLine.OnMouseOut -= PolyLine_OnMouseOut;
            polyLine.OnMouseOver -= PolyLine_OnMouseOver;
            polyLine.OnMouseUp -= PolyLine_OnMouseUp;
            polyLine.Dispose();
            changePolylineTimer?.Dispose();
        }
    }
}
