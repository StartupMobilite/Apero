using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;

namespace EventMyLife.ViewModel
{
    public class Geoloc
    {
        //emplacement geo en adresse
        public Geopoint myLocation = null;

        public async void refreshLoc()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    // Get the current location.
                    Geolocator geolocator = new Geolocator();
                    Debug.WriteLine("recup position");
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    myLocation = pos.Coordinate.Point;
                    Debug.WriteLine(myLocation.SpatialReferenceId);
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
        }

        public async void reverseGeocode(BasicGeoposition location)
        {

            Geopoint pointToReverseGeocode = new Geopoint(location);

            // Reverse geocode the specified geographic location.
            MapLocationFinderResult result =
                  await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);

            // If the query returns results, display the name of the town
            // contained in the address of the first result.
            if (result.Status == MapLocationFinderStatus.Success)
            {
                
            }
        }

        public async Task<string> reverseGeocode(Geopoint pointToReverseGeocode)
        {
            try
            {
                var mapLocation = await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);
                if (mapLocation.Status == MapLocationFinderStatus.Success)
                {
                    string address = mapLocation.Locations[0].Address.StreetNumber + " " + mapLocation.Locations[0].Address.Street;
                    return address;
                }
                else
                {
                    return "Not Found";
                }
            }
            catch
            {
                return "Impossible de trouvez votre adresse";
            }
        }

        public async Task<MapLocationFinderResult> geocode(string addressToGeocode, Geopoint hintPoint)
        {
            try {
               
                // Geocode the specified address, using the specified reference point
                // as a query hint. Return no more than 3 results.
                MapLocationFinderResult result =
                      await MapLocationFinder.FindLocationsAsync(
                                        addressToGeocode,
                                        hintPoint,
                                        3);

                // If the query returns results, display the coordinates
                // of the first result.
                if (result.Status == MapLocationFinderStatus.Success)
                {
                    Debug.WriteLine("reussi liste 1");
                    return result;
                }
            }
            catch
            {
                Debug.WriteLine("erreur recup liste 1");
            }
            return null;
            }


    }
}
