using DroneAPI.Data;
using Microsoft.AspNetCore.Http;

namespace DroneAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The context
        /// </summary>
        private DroneAPIDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public UnitOfWork(DroneAPIDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;

            DroneMedication = new DroneMedicationRepository(this.context, httpContextAccessor);
            Drone = new DroneRepository(this.context, httpContextAccessor);
            Medication = new MedicationRepository(this.context, httpContextAccessor);
        }

        /// <summary>
        /// Gets the drone medication.
        /// </summary>
        public IDroneMedicationRepository DroneMedication
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the drone.
        /// </summary>
        public IDroneRepository Drone
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the medication.
        /// </summary>
        public IMedicationRepository Medication
        {
            get;
            private set;
        }

        /// <summary>
        /// Save asynchronously.
        /// </summary>
        /// <returns>Task<int></returns>
        public async Task<int> SaveAsync()
            => await context.SaveChangesAsync();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
