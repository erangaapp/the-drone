using DroneAPI.Data;
using Microsoft.AspNetCore.Http;

namespace DroneAPI.Repositories
{
    /// <summary>
    /// The Drone Repository.
    /// </summary>
    /// <seealso cref="DroneAPIRepository&lt;DroneEntity&gt;" />
    /// <seealso cref="IDroneRepository" />
    public class DroneRepository : DroneAPIRepository<DroneEntity>, IDroneRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public DroneRepository(DroneAPIDbContext context, IHttpContextAccessor httpContextAccessor) :
            base(context, httpContextAccessor){ }

        /// <summary>
        /// Gets the available drones for loading.
        /// </summary>
        /// <returns>
        /// List of available Drone entities.
        /// </returns>
        public async Task<IEnumerable<DroneEntity>> GetAvailableDronesForLoading()
        {
            var drones = await FindAsync(x => x.DroneState == Core.DroneState.IDLE && 
            x.BatteryCapacity > Core.Constants.minimumBatteryCapacityToLoadADrone);
            return drones;
        }
    }
}
