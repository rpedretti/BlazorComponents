using RPedretti.Blazor.BingMap.Entities;
using RPedretti.Blazor.BingMap.Entities.InfoBox;
using RPedretti.Blazor.BingMap.Entities.Layer;
using RPedretti.Blazor.BingMap.Entities.Pushpin;
using RPedretti.Blazor.Shared.Collections;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMap.Sample.Pages.PushpinPage
{
    public class PushpinPageBase : BaseComponent, IDisposable
    {
        private int index = 0;
        private Random rnd = new Random();

        protected string BingMapId = $"bing-map-{Guid.NewGuid().ToString().Replace("-", "")}";
        protected BindingList<BaseBingMapEntity> Entities = new BindingList<BaseBingMapEntity>();
        protected BindingList<BingMapLayer> Layers = new BindingList<BingMapLayer>();
        private InfoBox infobox;
        protected BingMapLayer layer;
        private bool infoAttached;

        protected BingMapConfig MapsConfig { get; set; } = new BingMapConfig
        {
            MapTypeId = BingMapTypes.GrayScale,
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

        protected Task MapLoaded()
        {
            DisableAddButton = false;

            infobox = new InfoBox(new Geocoordinate(), new InfoboxOptions()
            {
                Visible = false,
                Title = "Position",
                ShowCloseButton = false,
                Offset = new GeolocatonPoint { X = 0, Y = 30 }
            });

            layer = new BingMapLayer("pushpins");
            Layers.Add(layer);

            return Task.CompletedTask;
        }

        public async Task AddPushpin()
        {
            if (!infoAttached)
            {
                await infobox.SetMap(BingMapId);
                infoAttached = true;
            }
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

            Entities.Add(pushpin);

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
            foreach (var entitie in Entities)
            {
                entitie.Dispose();
            }

            foreach (var layer in Layers)
            {
                layer.Dispose();
            }

            infobox?.Dispose();
            Entities = null;
        }
    }
}
