using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace EventMyLife.ViewModel
{
    class ViewModelLocator
    {

        public async void getPos()
        {
            //maj localisation
            Geolocator geoloc = new Geolocator();
            geoloc.DesiredAccuracyInMeters = 25;

            try
            {
                Geoposition pos = await geoloc.GetGeopositionAsync();
                ((double[])App.Current.Resources["myLocation"])[0] = pos.Coordinate.Point.Position.Latitude;
                ((double[])App.Current.Resources["myLocation"])[1] = pos.Coordinate.Point.Position.Longitude;
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("Géoloc interdite");
            }
        }

    }
}
