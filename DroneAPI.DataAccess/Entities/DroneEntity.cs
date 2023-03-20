using DroneAPI.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DroneAPI.Data
{
    /// <summary>
    /// The drone data entity.
    /// </summary>
    /// <seealso cref="EntityBase" />
    public class DroneEntity : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneEntity"/> class.
        /// </summary>
        public DroneEntity()
           => DroneMedications = new List<DroneMedicationEntity>();

        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        [MaxLength(100)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        public DroneModel DroneModel { get; set; }

        /// <summary>
        /// Gets or sets the weight limit.
        /// Consider as grams.
        /// </summary>
        public decimal WeightLimit { get; set; }

        /// <summary>
        /// Gets or sets the battery capacity.
        /// </summary>
        public decimal BatteryCapacity { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public DroneState DroneState { get; set; }

        /// <summary>
        /// Gets or sets the drone medications.
        /// </summary>
        public IEnumerable<DroneMedicationEntity> DroneMedications { get; set; }
    }
}
