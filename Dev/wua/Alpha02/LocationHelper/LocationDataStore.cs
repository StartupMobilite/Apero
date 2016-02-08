using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage;

namespace Location
{
    /// <summary>
    /// Provides access to stored location data. 
    /// </summary>
    public static class LocationDataStore
    {
        private const string dataFileName = "TrafficAppData.txt";

        /// <summary>
        /// Gets a list of four sample locations randomply positioned around the user's current 
        /// location or around the Microsoft main campus if the Geolocator is unavailable. 
        /// </summary>
        /// <returns>The sample locations.</returns>
        public static async Task<List<LocationData>> GetSampleLocationDataAsync()
        {
            var center = (await LocationHelper.GetCurrentLocationAsync())?.Position ?? 
                new BasicGeoposition { Latitude = 47.640068, Longitude = -122.129858 };

            int latitudeRange = 36000;
            int longitudeRange = 53000;
            var random = new Random();
            Func<int, double, double> getCoordinate = (range, midpoint) => 
                (random.Next(range) - (range / 2)) * 0.00001 + midpoint;

            var locations =
                (from name in new[] { "Event 1", "Event 2", "Event 3" }
                 select new LocationData
                 {
                     Name = name,
                     Position = new BasicGeoposition
                     {
                         Latitude = getCoordinate(latitudeRange, center.Latitude),
                         Longitude = getCoordinate(longitudeRange, center.Longitude)
                     }
                 }).ToList();

            await Task.WhenAll(locations.Select(async location =>
                await LocationHelper.TryUpdateMissingLocationInfoAsync(location, null)));

            return locations;
        }

        /// <summary>
        /// Load the saved location data from roaming storage. 
        /// </summary>
        public static async Task<List<LocationData>> GetLocationDataAsync()
        {
            List<LocationData> data = null;
            try
            {
                StorageFile dataFile = await ApplicationData.Current.RoamingFolder.GetFileAsync(dataFileName);
                string text = await FileIO.ReadTextAsync(dataFile);
                byte[] bytes = Encoding.Unicode.GetBytes(text);
                var serializer = new DataContractJsonSerializer(typeof(List<LocationData>));
                using (var stream = new MemoryStream(bytes))
                {
                    data = serializer.ReadObject(stream) as List<LocationData>;
                }
            }
            catch (FileNotFoundException)
            {
                // Do nothing.
            }
            return data ?? new List<LocationData>();
        }

        /// <summary>
        /// Save the location data to roaming storage. 
        /// </summary>
        /// <param name="locations">The locations to save.</param>
        public static async Task SaveLocationDataAsync(IEnumerable<LocationData> locations)
        {
            StorageFile sampleFile = await ApplicationData.Current.RoamingFolder.CreateFileAsync(
                dataFileName, CreationCollisionOption.ReplaceExisting);
            using (MemoryStream stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(List<LocationData>));
                serializer.WriteObject(stream, locations.ToList());
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string locationString = reader.ReadToEnd();
                    await FileIO.WriteTextAsync(sampleFile, locationString);
                }
            }
        }

    }
}
