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

namespace Alpha.Views
{ 

    public sealed partial class EventMap : Page
    {
        private Geopoint cityCenter;

        public EventMap()
        {
            this.InitializeComponent();

            // Add the MapControl and the specify maps authentication key.
            MapControl MapControl1 = new MapControl();
            MapControl1.ZoomInteractionMode = MapInteractionMode.GestureAndControl;
            MapControl1.TiltInteractionMode = MapInteractionMode.GestureAndControl;
            MapControl1.MapServiceToken = "NU84AueM7GMiPBOwHSoE~Ys6Syu5oct7d2PaDIdzl8g~AsQyIrLBonF8di9BqLCV0kg0tZnAFpbav2HztwHXPdUa3JOgL56tMLZP4E2vlTg8";
            pageGrid.Children.Add(MapControl1);
            MapControl1.Center = cityCenter;
            MapControl1.ZoomLevel = 12;
            MapControl1.LandmarksVisible = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };
            Geopoint cityCenter = new Geopoint(cityPosition);

            // Set the map location.
            MapControl1.Center = cityCenter;
            MapControl1.ZoomLevel = 12;
            MapControl1.LandmarksVisible = true;
        }

    }


}
