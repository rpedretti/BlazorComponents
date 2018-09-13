using RPedretti.Blazor.BingMap.Collections;
using RPedretti.Blazor.BingMap.Entities;
using RPedretti.Blazor.BingMap.Entities.InfoBox;
using RPedretti.Blazor.BingMap.Entities.Layer;
using RPedretti.Blazor.BingMap.Entities.Pushpin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Sample.Pages.PushpinPage
{
    public class PushpinPageBase : BaseComponent, IDisposable
    {
        private int index = 2;
        private Random rnd = new Random();

        protected string BingMapId = $"bing-map-{Guid.NewGuid().ToString().Replace("-", "")}";
        protected BingMapEntityList Entities = new BingMapEntityList();
        private InfoBox infobox;
        protected BingMapLayer layer;
        private bool infoAttached;

        protected BingMapConfig MapsConfig { get; set; } = new BingMapConfig
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

        protected BingMapsViewConfig MapsViewConfig { get; set; } = new BingMapsViewConfig();
        public bool DisableAddButton { get; set; }
        public int MapClick { get; private set; }
        public int MapRightClick { get; private set; }

        protected async Task MapLoaded()
        {
            DisableAddButton = false;

            infobox = new InfoBox(new Geocoordinate(), new InfoboxOptions()
            {
                Visible = false,
                Title = "Position",
                ShowCloseButton = false,
                Offset = new GeolocatonPoint { X = 0, Y = 30 }
            });

            if (!infoAttached)
            {
                await infobox.AttachMap(BingMapId);
                infoAttached = true;
            }

            Entities.AddRange(new BingMapPushpin[] {
                new BingMapPushpin() { Id = 0.ToString(), Center = new Geocoordinate() },
                new BingMapPushpin() { Id = 1.ToString(), Center = new Geocoordinate() { Latitude = 1 } },
                new BingMapPushpin() { Id = 2.ToString(), Center = new Geocoordinate() { Latitude = 2 } }
            });

            StateHasChanged();
        }

        public Task AddPushpin()
        {
            var pushpins = new List<BingMapPushpin>();
            for (int i = 0; i < 3; i++)
            {

                index++;

                var latitude = rnd.NextDouble() * -180 + 90;
                var longitude = rnd.NextDouble() * -360 + 180;

                var pushpin = new BingMapPushpin
                {
                    Id = index.ToString(),
                    Center = new Geocoordinate { Latitude = latitude, Longitude = longitude },
                    Options = new PushpinOptions
                    {
                        Text = index.ToString(),
                        Draggable = true,
                        Icon = "https://www.bingmapsportal.com/Content/images/poi_custom.png"
                    }
                };

                pushpin.OnMouseOver += Pushpin_OnMouseOver;
                pushpin.OnMouseOut += Pushpin_OnMouseOut;
                pushpin.OnDragEnd += Pushpin_OnDragEnd;
                pushpin.OnDragStart += Pushpin_OnDragStart;

                pushpins.Add(pushpin);
            }

            Entities.AddRange(pushpins);

            return Task.CompletedTask;

        }

        private async void Pushpin_OnDragStart(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            await infobox.Options(new InfoboxOptions
            {
                Visible = false
            });
        }

        private void Pushpin_OnDragEnd(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            StateHasChanged();
        }

        public void ToggleVisibility(BingMapPushpin pushpin)
        {
            var visible = !pushpin.OptionsSnapshot.Visible ?? false;
            pushpin.Options = new PushpinOptions
            {
                Visible = visible
            };

            StateHasChanged();
        }

        public void RemovePushpin(BingMapPushpin pushpin)
        {
            pushpin.OnMouseOver -= Pushpin_OnMouseOver;
            pushpin.OnMouseOut -= Pushpin_OnMouseOut;
            pushpin.OnDragEnd -= Pushpin_OnDragEnd;
            pushpin.OnDragStart -= Pushpin_OnDragStart;

            Entities.Remove(pushpin);
        }

        private async void Pushpin_OnMouseOver(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            var location = (sender as BingMapPushpin).Center;
            await infobox.Options(new InfoboxOptions
            {
                Visible = true,
                Description = $"<p>{location.Latitude}</p><p>{location.Longitude}</p>",
                Location = new Location(location.Latitude, location.Longitude),
            });
        }

        private async void Pushpin_OnMouseOut(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            await infobox.Options(new InfoboxOptions
            {
                Visible = false
            });
        }

        public void Dispose()
        {
            foreach (var item in Entities)
            {
                item.Dispose();
            }

            infobox?.Dispose();
        }

        public Task OnMapRightClick(MouseEventArgs<BingMapBase> args)
        {
            MapRightClick++;
            StateHasChanged();
            return Task.CompletedTask;
        }

        public Task OnMapClick(MouseEventArgs<BingMapBase> args)
        {
            MapClick++;
            StateHasChanged();
            return Task.CompletedTask;
        }

        public Task OnMapThrottleViewChangeEnd()
        {
            Console.WriteLine("Throttle View Change End");
            return Task.CompletedTask;
        }
    }
}
