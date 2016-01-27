using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI;

namespace Alpha.Views
{

    public sealed partial class EventMap : Page
    {

        public EventMap()
        {
            this.InitializeComponent();

            // Add the MapControl and the specify maps authentication key.
            MapControl MapControl2 = new MapControl();
            MapControl2.ZoomInteractionMode = MapInteractionMode.GestureAndControl;
            MapControl2.TiltInteractionMode = MapInteractionMode.GestureAndControl;
            MapControl2.MapServiceToken = "NU84AueM7GMiPBOwHSoE~Ys6Syu5oct7d2PaDIdzl8g~AsQyIrLBonF8di9BqLCV0kg0tZnAFpbav2HztwHXPdUa3JOgL56tMLZP4E2vlTg8";
            pageGrid.Children.Add(MapControl2);

//            MapControl1.Center = cityCenter;
            MapControl1.ZoomLevel = 18;
            MapControl1.LandmarksVisible = true;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 48.869465, Longitude = 2.394892 };
            Geopoint cityCenter = new Geopoint(cityPosition);

            // Set the map location.
            MapControl1.Center = cityCenter;
            MapControl1.ZoomLevel = 18;
            MapControl1.LandmarksVisible = true;

            // Set your current location.
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    // Get the current location.
                    Geolocator geolocator = new Geolocator();
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    Geopoint myLocation = pos.Coordinate.Point;

                    // Set the map location.
                    MapControl1.Center = myLocation;
                    MapControl1.ZoomLevel = 12;
                    MapControl1.LandmarksVisible = true;
                    break;

                case GeolocationAccessStatus.Denied:
                    // Handle the case  if access to location is denied.
                    break;

                case GeolocationAccessStatus.Unspecified:
                    // Handle the case if  an unspecified error occurs.
                    break;
            }
        }

        private async void showStreetsideView()
        {
            // Check if Streetside is supported.
            if (MapControl1.IsStreetsideSupported)
            {
                // Find a panorama near Avenue Gustave Eiffel.
                BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 48.869465, Longitude = 2.394892 };
                Geopoint cityCenter = new Geopoint(cityPosition);
                StreetsidePanorama panoramaNearCity = await StreetsidePanorama.FindNearbyAsync(cityCenter);

                // Set the Streetside view if a panorama exists.
                if (panoramaNearCity != null)
                {
                    // Create the Streetside view.
                    StreetsideExperience ssView = new StreetsideExperience(panoramaNearCity);
                    ssView.OverviewMapVisible = true;
                    MapControl1.CustomExperience = ssView;
                }
            }
            else
            {
                // If Streetside is not supported
                ContentDialog viewNotSupportedDialog = new ContentDialog()
                {
                    Title = "Streetside is not supported",
                    Content = "\nStreetside views are not supported on this device.",
                    PrimaryButtonText = "OK"
                };
                await viewNotSupportedDialog.ShowAsync();
            }
        }

        private async void display3DLocation()
        {
            if (MapControl1.Is3DSupported)
            {
                // Set the aerial 3D view.
                MapControl1.Style = MapStyle.Aerial3DWithRoads;

                // Specify the location.
                BasicGeoposition hwGeoposition = new BasicGeoposition() { Latitude = 48.869465, Longitude = 2.394892 };
                Geopoint hwPoint = new Geopoint(hwGeoposition);

                // Create the map scene.
                MapScene hwScene = MapScene.CreateFromLocationAndRadius(hwPoint,
                                                                                     80, /* show this many meters around */
                                                                                     0, /* looking at it to the North*/
                                                                                     60 /* degrees pitch */);
                // Set the 3D view with animation.
                await MapControl1.TrySetSceneAsync(hwScene, MapAnimationKind.Bow);
            }
            else
            {
                // If 3D views are not supported, display dialog.
                ContentDialog viewNotSupportedDialog = new ContentDialog()
                {
                    Title = "3D is not supported",
                    Content = "\n3D views are not supported on this device.",
                    PrimaryButtonText = "OK"
                };
                await viewNotSupportedDialog.ShowAsync();
            }
        }

        private void displayPOIButton_Click(object sender, RoutedEventArgs e)
        {
            // Specify a known location.
            BasicGeoposition snPosition = new BasicGeoposition() { Latitude = 47.620, Longitude = -122.349 };
            Geopoint snPoint = new Geopoint(snPosition);

            // Create a MapIcon.
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Location = snPoint;
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.Title = "Space Needle";
            mapIcon1.ZIndex = 0;

            //Add Image to MapIcon
            mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/customicon.png"));

            // Add the MapIcon to the map.
            MapControl1.MapElements.Add(mapIcon1);

            // Center the map over the POI.
            MapControl1.Center = snPoint;
            MapControl1.ZoomLevel = 14;

        }

        private void displayXAMLButton_Click(object sender, RoutedEventArgs e)
        {
            // Specify a known location.
            BasicGeoposition snPosition = new BasicGeoposition() { Latitude = 47.620, Longitude = -122.349 };
            Geopoint snPoint = new Geopoint(snPosition);

            // Create a XAML border.
            Border border = new Border
            {
                Height = 100,
                Width = 100,
                BorderBrush = new SolidColorBrush(Windows.UI.Colors.Blue),
                BorderThickness = new Thickness(5),
            };

            // Center the map over the POI.
            MapControl1.Center = snPoint;
            MapControl1.ZoomLevel = 18;

            // Add XAML to the map.
            MapControl1.Children.Add(border);
            MapControl.SetLocation(border, snPoint);
            MapControl.SetNormalizedAnchorPoint(border, new Point(0.5, 0.5));
        }

        private void mapPolygonAddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            double centerLatitude = MapControl1.Center.Position.Latitude;
            double centerLongitude = MapControl1.Center.Position.Longitude;
            MapPolygon mapPolygon = new MapPolygon();
            mapPolygon.Path = new Geopath(new List<BasicGeoposition>() {
         new BasicGeoposition() {Latitude=centerLatitude+0.0005, Longitude=centerLongitude-0.001 },
         new BasicGeoposition() {Latitude=centerLatitude-0.0005, Longitude=centerLongitude-0.001 },
         new BasicGeoposition() {Latitude=centerLatitude-0.0005, Longitude=centerLongitude+0.001 },
         new BasicGeoposition() {Latitude=centerLatitude+0.0005, Longitude=centerLongitude+0.001 },

   });

            mapPolygon.ZIndex = 1;
            mapPolygon.FillColor = Colors.Red;
            mapPolygon.StrokeColor = Colors.Blue;
            mapPolygon.StrokeThickness = 3;
            mapPolygon.StrokeDashed = false;
            MapControl1.MapElements.Add(mapPolygon);
        }

        private void mapPolylineAddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            double centerLatitude = MapControl1.Center.Position.Latitude;
            double centerLongitude = MapControl1.Center.Position.Longitude;
            MapPolyline mapPolyline = new MapPolyline();
            mapPolyline.Path = new Geopath(new List<BasicGeoposition>() {
         new BasicGeoposition() {Latitude=centerLatitude-0.0005, Longitude=centerLongitude-0.001 },
         new BasicGeoposition() {Latitude=centerLatitude+0.0005, Longitude=centerLongitude+0.001 },
   });

            mapPolyline.StrokeColor = Colors.Black;
            mapPolyline.StrokeThickness = 3;
            mapPolyline.StrokeDashed = true;
            MapControl1.MapElements.Add(mapPolyline);
        }

    }


}
