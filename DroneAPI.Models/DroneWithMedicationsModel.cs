namespace DroneAPI.Models
{
    /// <summary>
    /// Drone With Medications Model
    /// </summary>
    /// <seealso cref="DroneAPI.Models.DroneModel" />
    public class DroneWithMedicationsModel : DroneModel
    {
        /// <summary>
        /// Gets or sets the medications.
        /// </summary>
        public IList<MedicationModel> Medications { get; set; }
    }
}
