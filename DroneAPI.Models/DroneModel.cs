namespace DroneAPI.Models
{
    public class DroneModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the weight limit.
        /// </summary>
        public decimal WeightLimit { get; set; }

        /// <summary>
        /// Gets or sets the battery capacity.
        /// </summary>
        public string BatteryCapacity { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }
    }
}
