using DroneAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneAPI.Services
{
    /// <summary>
    /// The Medication Service.
    /// </summary>
    public interface IMedicationService
    {
        /// <summary>
        /// Gets the medications.
        /// </summary>
        /// <param name="medicationIds">The medication ids.</param>
        /// <returns></returns>
        Task<IList<MedicationModel>> GetMedications(IList<int> medicationIds);
    }
}
