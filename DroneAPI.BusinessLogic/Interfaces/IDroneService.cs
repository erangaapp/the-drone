using DroneAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneAPI.Services
{
    /// <summary>
    /// The drone service
    /// </summary>
    public interface IDroneService
    {
        /// <summary>
        /// Registers the drone.
        /// </summary>
        /// <param name="drone">The drone.</param>
        /// <returns>The registered drone.</returns>
        Task<DroneModel> RegisterDrone(DroneRegisterModel drone);

        /// <summary>
        /// Updates the state of the drone.
        /// </summary>
        /// <param name="droneId">The drone identifier.</param>
        /// <param name="state">The state.</param>
        Task UpdateDroneState(int droneId, Core.DroneState state);

        /// <summary>
        /// Loads the drone with medication items.
        /// </summary>
        /// <param name="droneId">The drone identifier.</param>
        /// <param name="medicationIds">The medication ids.</param>
        /// <returns>Loaded drone with medications.</returns>
        Task<DroneWithMedicationsModel> LoadDroneWithMedicationItems(int droneId, IList<int> medicationIds);

        /// <summary>
        /// Gets the loaded medication items.
        /// </summary>
        /// <param name="droneId">The drone identifier.</param>
        /// <returns>Get the loaded medication items for the given drone.</returns>
        Task<IList<MedicationModel>> GetLoadedMedicationItems(int droneId);

        /// <summary>
        /// Gets the available drones for loading.
        /// </summary>
        /// <returns>List of available drones for loading.</returns>
        Task<IList<DroneModel>> GetAvailableDronesForLoading();

        /// <summary>
        /// Gets the drone battery level.
        /// </summary>
        /// <param name="droneId">The drone identifier.</param>
        /// <returns>The drone battery level.</returns>
        Task<string> GetDroneBatteryLevel(int droneId);
    }
}
