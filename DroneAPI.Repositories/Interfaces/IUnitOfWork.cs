namespace DroneAPI.Repositories
{
    /// <summary>
    /// The Unit of work.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the drone medication.
        /// </summary>
        IDroneMedicationRepository DroneMedication { get; }

        /// <summary>
        /// Gets the drone.
        /// </summary>
        IDroneRepository Drone { get; }

        /// <summary>
        /// Gets the medication.
        /// </summary>
        IMedicationRepository Medication { get; }

        /// <summary>
        /// Save asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAsync();
    }
}
