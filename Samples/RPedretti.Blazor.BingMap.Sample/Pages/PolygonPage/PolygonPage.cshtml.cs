using RPedretti.Blazor.BingMap.Collections;
using RPedretti.Blazor.BingMap.Entities;
using RPedretti.Blazor.BingMap.Entities.Polygon;
using RPedretti.Blazor.Shared.Operators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Sample.Pages.PolygonPage
{
    public class PolygonPageBase : BaseComponent, IDisposable
    {
        protected string BingMapId = $"bing-map-{Guid.NewGuid().ToString().Replace("-", "")}";
        protected BingMapEntityList Entities = new BingMapEntityList();
        protected DebounceDispatcher clickDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher doubleClickDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher downDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher upDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher outDispatcher = new DebounceDispatcher();

        private BingMapPolygon polygon;
        private Timer changePolygonTimer;

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
            Zoom = 10,
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

            polygon = new BingMapPolygon
            {
                Coordinates = new BindingList<Geocoordinate>
                {
                    new Geocoordinate { Latitude = 0.1, Longitude = -0.1 },
                    new Geocoordinate { Latitude = 0.1, Longitude = 0.1 },
                    new Geocoordinate { Latitude = -0.1, Longitude = 0.1 },
                    new Geocoordinate { Latitude = -0.1, Longitude = -0.1 }
                },
                Options = new BingMapPolygonOptions
                {
                    FillColor = Color.Blue,
                    StrokeThickness = 2,
                    StrokeColor = Color.Red
                }
            };

            polygon.OnClick += Polygon_OnClick;
            polygon.OnDoubleClick += Polygon_OnDoubleClick;
            polygon.OnMouseDown += Polygon_OnMouseDown;
            polygon.OnMouseOut += Polygon_OnMouseOut;
            polygon.OnMouseOver += Polygon_OnMouseOver;
            polygon.OnMouseUp += Polygon_OnMouseUp;

            Entities.Add(polygon);

            changePolygonTimer = new Timer((o) =>
            {
                polygon.Rings = new BindingList<Geocoordinate[]>
                {
                    new Geocoordinate[]
                    {
                        new Geocoordinate { Latitude = 0.1, Longitude = -0.1 },
                        new Geocoordinate { Latitude = 0.1, Longitude = 0.1 },
                        new Geocoordinate { Latitude = -0.1, Longitude = 0.1 },
                        new Geocoordinate { Latitude = -0.1, Longitude = -0.1 }
                    },
                    new Geocoordinate[]
                    {
                        new Geocoordinate { Latitude = 0.05, Longitude = -0.05 },
                        new Geocoordinate { Latitude = -0.05, Longitude = -0.05 },
                        new Geocoordinate { Latitude = -0.05, Longitude = 0.05 },
                        new Geocoordinate { Latitude = 0.05, Longitude = 0.05 }
                    }
                };
                StateHasChanged();
            }, null, 3000, Timeout.Infinite);

            return Task.CompletedTask;
        }

        private void Polygon_OnMouseUp(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            MouseUp = true;
            upDispatcher.Debounce(2000, (o) => { MouseUp = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void Polygon_OnMouseOver(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            MouseOver = true;
            MouseOut = false;
            StateHasChanged();
        }

        private void Polygon_OnMouseOut(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            MouseOver = false;
            MouseOut = true;
            outDispatcher.Debounce(2000, (o) => { MouseOut = false; StateHasChanged(); });

            StateHasChanged();
        }

        private void Polygon_OnMouseDown(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            MouseDown = true;
            downDispatcher.Debounce(2000, (o) => { MouseDown = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void Polygon_OnDoubleClick(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            DoubleClick = true;
            doubleClickDispatcher.Debounce(2000, (o) => { DoubleClick = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void Polygon_OnClick(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            Click = true;
            clickDispatcher.Debounce(2000, (o) => { Click = false; StateHasChanged(); });
            StateHasChanged();
        }

        public void Dispose()
        {
            changePolygonTimer?.Dispose();
            polygon.OnClick -= Polygon_OnClick;
            polygon.OnDoubleClick -= Polygon_OnDoubleClick;
            polygon.OnMouseDown -= Polygon_OnMouseDown;
            polygon.OnMouseOut -= Polygon_OnMouseOut;
            polygon.OnMouseOver -= Polygon_OnMouseOver;
            polygon.OnMouseUp -= Polygon_OnMouseUp;
            polygon.Dispose();
        }
    }

}
