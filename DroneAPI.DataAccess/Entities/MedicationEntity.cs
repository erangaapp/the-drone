using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DroneAPI.Data
{
    /// <summary>
    /// The medication data entity.
    /// </summary>
    /// <seealso cref="EntityBase" />
    public class MedicationEntity : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationEntity"/> class.
        /// </summary>
        public MedicationEntity()
           => DroneMedications = new List<DroneMedicationEntity>();

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [RegularExpression("[a-zA-Z0-9_-]+$")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [RegularExpression("[A-Z0-9_]+$")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the drone medications.
        /// </summary>
        public IEnumerable<DroneMedicationEntity> DroneMedications { get; set; }
    }
}
