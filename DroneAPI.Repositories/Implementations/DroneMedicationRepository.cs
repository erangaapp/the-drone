using DroneAPI.Data;
using Microsoft.AspNetCore.Http;

namespace DroneAPI.Repositories
{
    /// <summary>
    /// The Drone Medication Repository.
    /// </summary>
    /// <seealso cref="DroneAPIRepository&lt;DroneMedicationEntity&gt;" />
    /// <seealso cref="IDroneMedicationRepository" />
    public class DroneMedicationRepository : DroneAPIRepository<DroneMedicationEntity>, IDroneMedicationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneMedicationRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public DroneMedicationRepository(DroneAPIDbContext context, IHttpContextAccessor httpContextAccessor) : 
            base(context, httpContextAccessor) { }
    }
}
