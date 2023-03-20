using System;

namespace DroneAPI.Data
{
    /// <summary>
    /// The drone medication entity.
    /// </summary>
    /// <seealso cref="EntityBase" />
    public class DroneMedicationEntity : EntityBase
    {
        /// <summary>
        /// Gets or sets the drone identifier.
        /// </summary>
        public int DroneId { get; set; }

        /// <summary>
        /// Gets or sets the medication identifier.
        /// </summary>
        public int MedicationId { get; set; }

        /// <summary>
        /// Gets or sets the delivery completed date time.
        /// </summary>
        public DateTime? DeliveryCompletedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the drone.
        /// </summary>
        public DroneEntity Drone { get; set; }

        /// <summary>
        /// Gets or sets the medication.
        /// </summary>
        public MedicationEntity Medication { get; set; }
    }
}
