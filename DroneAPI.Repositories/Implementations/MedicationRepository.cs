using DroneAPI.Data;
using Microsoft.AspNetCore.Http;

namespace DroneAPI.Repositories
{
    /// <summary>
    /// The Medication Repository.
    /// </summary>
    /// <seealso cref="DroneAPIRepository&lt;MedicationEntity&gt;" />
    /// <seealso cref="IMedicationRepository" />
    public class MedicationRepository : DroneAPIRepository<MedicationEntity>, IMedicationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public MedicationRepository(DroneAPIDbContext context, IHttpContextAccessor httpContextAccessor) : 
            base(context, httpContextAccessor) { }
    }
}
