using System.ComponentModel.DataAnnotations;

namespace DroneAPI.Models
{
    public class DroneRegisterModel
    {
        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        [Required]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the weight limit.
        /// </summary>
        [Required]
        public decimal WeightLimit { get; set; }
    }
}
