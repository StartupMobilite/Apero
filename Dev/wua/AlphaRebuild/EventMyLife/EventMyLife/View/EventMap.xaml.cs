using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace EventMyLife.ViewModel
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class EventMap : Page
    {
        public Geopoint myLocation =null;
        public Geoloc gs = new Geoloc();
        public EventMap()
        {
            this.InitializeComponent();
        }

        public async void AddMapIcon(Geopoint snPoint)
        {
            try
            {
                myLocation = await refreshLoc();
                if (myLocation != null)
                {
                    string adresse = await gs.reverseGeocode(myLocation);
                    MapControl1.Center = myLocation;
                    //set a map icon start
                    MapIcon mapIconStart = new MapIcon();
                    mapIconStart.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/map-pin.png"));
                    mapIconStart.Location = myLocation;
                    mapIconStart.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    mapIconStart.Title = adresse;
                    mapIconStart.ZIndex = 1;
                    MapControl1.MapElements.Add(mapIconStart);
                    MapControl1.ZoomLevel = 14;
                }
                else
                {
                    Debug.WriteLine("Erreur geoloc Null");
                }
            }
            catch
            {

            }
        }

        public async void setCurrentPos()
        {
            try
            {
                myLocation = await refreshLoc();
                if (myLocation != null)
                {
                    string adresse = await gs.reverseGeocode(myLocation);
                    MapControl1.Center = myLocation;
                    //set a map icon start
                    MapIcon mapIconStart = new MapIcon();
                    mapIconStart.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/map-pin.png"));
                    mapIconStart.Location = myLocation;
                    mapIconStart.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    mapIconStart.Title = string.Format("vous :\n{0}",adresse);
                    mapIconStart.ZIndex = 1;
                    MapControl1.MapElements.Add(mapIconStart);
                    MapControl1.ZoomLevel = 14;
                }
                else
                {
                    Debug.WriteLine("Erreur geoloc Null");
                }
            }
            catch
            {

            }

        }

        public async Task<Geopoint> refreshLoc()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            Geopoint loctmp = null;
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    // Get the current location.
                    Geolocator geolocator = new Geolocator();
                    Debug.WriteLine("recup");
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    loctmp = pos.Coordinate.Point;
                    Debug.WriteLine(myLocation);
                    break;

                case GeolocationAccessStatus.Denied:
                    // Handle the case  if access to location is denied.
                    Debug.WriteLine("Geoloc denied");
                    break;

                case GeolocationAccessStatus.Unspecified:
                    Debug.WriteLine("Error unknow");
                    // Handle the case if  an unspecified error occurs.
                    break;
            }
            return loctmp;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            setCurrentPos();
        }

    }
}
