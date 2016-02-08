using Location;
using Windows.ApplicationModel.Background;

namespace TrafficMonitor
{
    /// <summary>
    /// Represents a background task that monitors selected locations for an 
    /// increase in travel time due to traffic. 
    /// </summary>
    public sealed class TrafficMonitor : IBackgroundTask
    {
        /// <summary>
        /// Uses the LocationHelper class to check monitored locations
        /// and raise a notification if the travel time has increased. 
        /// </summary>
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            bool isCanceled = false;
            taskInstance.Canceled += (s, e) => isCanceled = true;
            try
            {
                if (isCanceled)
                {
                    deferral.Complete();
                    return;
                }
                await LocationHelper.CheckTravelInfoForMonitoredLocationsAsync();
            }
            finally
            {
                deferral.Complete();
            }
        }
    }

}
