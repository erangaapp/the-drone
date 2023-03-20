using DroneAPI.Core;
using DroneAPI.Data;

namespace DroneAPI.Repositories
{
    /// <summary>
    /// The Drone Repository
    /// </summary>
    /// <seealso cref="DroneAPI.Core.IGenericRepository&lt;DroneAPI.Data.DroneEntity&gt;" />
    public interface IDroneRepository : IGenericRepository<DroneEntity>
    {
        /// <summary>
        /// Gets the available drones for loading.
        /// </summary>
        /// <returns>List of available Drone entities.</returns>
        Task<IEnumerable<DroneEntity>> GetAvailableDronesForLoading();
    }
}
