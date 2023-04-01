using DroneAPI.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Schedule.CheckDronesBatteryLevel
{
    public class Functions
    {
        public static async void CheckDronesBatteryLevels([TimerTrigger("00:00:10")] TimerInfo timer,ILogger logger, IDroneService droneService)
        {
            var availableDrones = await droneService.GetAvailableDronesForLoading();
            foreach (var drone in availableDrones)
            {
                var batteryLevel = await droneService.GetDroneBatteryLevel(drone.Id);
                logger.LogInformation($"Battery Level Of Drone: {drone.SerialNumber} is {batteryLevel}");
            }
        }
    }
}
