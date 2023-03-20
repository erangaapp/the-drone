using AutoMapper;
using DroneAPI.Models;
using DroneAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneAPI.Services
{
    /// <summary>
    /// The Medication Service
    /// </summary>
    /// <seealso cref="IMedicationService" />
    public class MedicationService : IMedicationService
    {
        /// <summary>
        /// The medication repository
        /// </summary>
        private readonly IMedicationRepository medicationRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationService"/> class.
        /// </summary>
        /// <param name="medicationRepository">The medication repository.</param>
        /// <param name="mapper">The mapper.</param>
        public MedicationService(IMedicationRepository medicationRepository,
            IMapper mapper)
        {
            this.medicationRepository = medicationRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the medications.
        /// </summary>
        /// <param name="medicationIds">The medication ids.</param>
        /// <returns>The List of medications.</returns>
        public async Task<IList<MedicationModel>> GetMedications(IList<int> medicationIds)
        {
            var medications = await this.medicationRepository.
                FindAsync(medication => medicationIds.Contains(medication.Id));

            return mapper.Map<IList<MedicationModel>>(medications.ToList());
        }
    }
}
