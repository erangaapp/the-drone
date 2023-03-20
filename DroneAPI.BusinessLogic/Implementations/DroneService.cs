using AutoMapper;
using DroneAPI.Data;
using DroneAPI.Models;
using DroneAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DroneAPI.Services
{
    public class DroneService : IDroneService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDroneRepository droneRepository;
        private readonly IMedicationRepository medicationRepository;
        private readonly IDroneMedicationRepository droneMedicationRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="droneRepository">The drone repository.</param>
        /// <param name="medicationRepository">The medication repository.</param>
        /// <param name="droneMedicationRepository">The drone medication repository.</param>
        /// <param name="mapper">The mapper.</param>
        public DroneService(IUnitOfWork unitOfWork,
            IDroneRepository droneRepository,
            IMedicationRepository medicationRepository,
            IDroneMedicationRepository droneMedicationRepository,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.droneRepository = droneRepository;
            this.medicationRepository = medicationRepository;
            this.droneMedicationRepository = droneMedicationRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Loads the drone with medication items.
        /// </summary>
        /// <param name="droneId">The drone identifier.</param>
        /// <param name="medicationIds">The medication ids.</param>
        /// <returns>
        /// Loaded drone with medications.
        /// </returns>
        public async Task<DroneWithMedicationsModel> LoadDroneWithMedicationItems(int droneId, IList<int> medicationIds)
        {
            var drone = await this.droneRepository.GetByIdAsync(droneId);
            var medicationsToLoad = await medicationRepository.FindAsync(medication => medicationIds.Contains(medication.Id));
            var validationMessage = ValidateLoadDroneWithMedicationItems(drone, medicationsToLoad);
            if (!string.IsNullOrEmpty(validationMessage))
                throw new Exception(validationMessage);

            var droneMedications = medicationIds.Select(medicationId => new DroneMedicationEntity()
            {
                DroneId = droneId,
                MedicationId = medicationId
            });

            droneMedicationRepository.AddRange(droneMedications);

            drone.BatteryCapacity -= Core.Constants.minimumBatteryCapacityToLoadADrone;
            drone.DroneState = Core.DroneState.LOADED;
            await droneRepository.UpdateAsync(drone.Id, drone);

            await unitOfWork.SaveAsync();

            var loadedDron = await this.droneRepository.
                FindAsync(drone => drone.Id == droneId, 
                includeProperties: nameof(drone.DroneMedications));

            return mapper.Map<DroneWithMedicationsModel>(loadedDron.FirstOrDefault());
        }

        /// <summary>
        /// Validates the load drone with medication items.
        /// </summary>
        /// <param name="drone">The drone.</param>
        /// <param name="medications">The medications.</param>
        /// <returns>Validation error messages.</returns>
        private string ValidateLoadDroneWithMedicationItems(DroneEntity drone, IEnumerable<MedicationEntity> medications)
        {
            StringBuilder messages = new StringBuilder();

            if (drone == null)
                messages.Append(ModelTexts.Message_DroneNotFound);
            if (!medications.Any())
                messages.Append(ModelTexts.Message_MedicationsNotFound);
            if (drone.DroneState != Core.DroneState.IDLE ||
                drone.WeightLimit < medications.Sum(md => md.Weight) ||
                drone.BatteryCapacity < Core.Constants.minimumBatteryCapacityToLoadADrone)
                messages.Append($"{ModelTexts.Message_DroneNotSuitableForCaringTheLoad}{Environment.NewLine}" +
                    $"{string.Format(ModelTexts.Message_DroneProperties, drone.DroneState, drone.WeightLimit, drone.BatteryCapacity)}");

            return messages.ToString();
        }

        /// <summary>
        /// Gets the loaded medication items.
        /// </summary>
        /// <param name="droneId">The drone identifier.</param>
        /// <returns>
        /// Get the loaded medication items for the given drone.
        /// </returns>
        public async Task<IList<MedicationModel>> GetLoadedMedicationItems(int droneId)
        {
            var dronMedications = await this.droneMedicationRepository.
                FindAsync(droneMedication => droneMedication.DroneId == droneId &&
                droneMedication.DeliveryCompletedDateTime == null, includeProperties: "Medication");

            var medications = dronMedications.Select(dronMedication => dronMedication.Medication).ToList();
            return mapper.Map<IList<MedicationModel>>(medications);
        }

        /// <summary>
        /// Registers the drone.
        /// </summary>
        /// <param name="drone">The drone.</param>
        /// <returns>
        /// The registered drone.
        /// </returns>
        public async Task<DroneModel> RegisterDrone(DroneRegisterModel drone)
        {
            var droneEntity = mapper.Map<DroneEntity>(drone);

            droneRepository.Add(droneEntity);
            await unitOfWork.SaveAsync();

            return mapper.Map<DroneModel>(droneEntity);
        }

        /// <summary>
        /// Updates the state of the drone.
        /// </summary>
        /// <param name="droneId">The drone identifier.</param>
        /// <param name="state">The state.</param>
        public async Task UpdateDroneState(int droneId, Core.DroneState state)
        {
            var drone = await droneRepository.
                GetByIdAsync(droneId);
            if (drone == null)
                throw new Exception(ModelTexts.Message_DroneNotFound);

            drone.DroneState = state;
            await droneRepository.UpdateAsync(droneId, drone);
            await unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Gets the available drones for loading.
        /// </summary>
        /// <returns>
        /// List of available drones for loading.
        /// </returns>
        public async Task<IList<DroneModel>> GetAvailableDronesForLoading()
        {
            var drones = await this.droneRepository.GetAvailableDronesForLoading();
            return mapper.Map<IList<DroneModel>>(drones);
        }

        /// <summary>
        /// Gets the drone battery level.
        /// </summary>
        /// <param name="droneId">The drone identifier.</param>
        /// <returns>
        /// The drone battery level.
        /// </returns>
        public async Task<string> GetDroneBatteryLevel(int droneId)
        {
            var drone = await this.droneRepository.GetByIdAsync(droneId);
            if(drone == null) return null;

            var droneModel = mapper.Map<DroneModel>(drone);
            return droneModel.BatteryCapacity;
        }
    }
}
