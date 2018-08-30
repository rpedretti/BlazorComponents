using Newtonsoft.Json;
using RPedretti.Blazor.BingMaps.Entities;
using RPedretti.Blazor.BingMaps.Entities.InfoBox;
using RPedretti.Blazor.BingMaps.Entities.Pushpin;
using RPedretti.Blazor.Shared.Collections;
using System;
using System.Threading.Tasks;

namespace RPedretti.Blazor.BingMaps.Sample.Pages.Pushpin
{
    public class PushpinPageBase : BaseComponent, IDisposable
    {
        private int index = 0;
        private Random rnd = new Random();

        protected string BingMapId = $"bing-maps-{Guid.NewGuid().ToString().Replace("-", "")}";
        protected BindingList<BaseBingMapEntity> Entities = new BindingList<BaseBingMapEntity>();
        private InfoBox infobox;
        private bool infoAttached;

        protected BingMapsConfig MapsConfig { get; set; } = new BingMapsConfig
        {
            MapTypeId = BingMapsTypes.GrayScale,
            SupportedMapTypes = new string[] {
                BingMapsTypes.Aerial,
                BingMapsTypes.GrayScale,
                BingMapsTypes.Road,
                BingMapsTypes.BirdsEyes
            },
            Center = new Geocoordinate { Latitude = 0, Longitude = 0 },
            EnableHighDpi = true,
            Zoom = 2,
            ShowTrafficButton = true
        };

        protected BingMapsViewConfig MapsViewConfig { get; set; } = new BingMapsViewConfig();
        public bool DisableAddButton { get; set; }

        protected void MapLoaded()
        {
            Console.WriteLine($"loaded {BingMapId}");
            DisableAddButton = false;

            infobox = new InfoBox(new Geocoordinate(), new InfoboxOptions()
            {
                Visible = false,
                Title = "Position",
                ShowCloseButton = false,
                Offset = new GeolocatonPoint { X = 0, Y = 30 }
            });

            StateHasChanged();
        }

        public async Task AddPushpin()
        {
            if (!infoAttached)
            {
                await infobox.SetMap(BingMapId);
                infoAttached = true;
            }
            index++;
            var pushpin = new BingMapPushpin
            {
                Id = index.ToString()
            };

            var latitude = rnd.NextDouble() * -180 + 90;
            var longitude = rnd.NextDouble() * -360 + 180;

            pushpin.Center = new Geocoordinate { Latitude = latitude, Longitude = longitude };
            pushpin.Options = new PushpinOptions
            {
                Text = index.ToString(),
                Icon = "https://www.bingmapsportal.com/Content/images/poi_custom.png"
            };

            pushpin.OnMouseOver += Pushpin_OnMouseOver;
            pushpin.OnMouseOut += Pushpin_OnMouseOut;

            Entities.Add(pushpin);

        }

        public void RemovePushpin(BingMapPushpin pushpin)
        {
            pushpin.OnMouseOver -= Pushpin_OnMouseOver;
            pushpin.OnMouseOut -= Pushpin_OnMouseOut;

            Entities.Remove(pushpin);
        }

        private void Pushpin_OnMouseOver(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            infobox.Options(new InfoboxOptions
            {
                Visible = true,
                Description = $"<p>{e.Target.Center.Latitude}</p><p>{e.Target.Center.Longitude}</p>",
                Location = new Location(e.Target.Center.Latitude, e.Target.Center.Longitude),
            });
        }

        private void Pushpin_OnMouseOut(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            infobox.Options(new InfoboxOptions
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

            infobox?.Dispose();
            Entities = null;
        }
    }
}
