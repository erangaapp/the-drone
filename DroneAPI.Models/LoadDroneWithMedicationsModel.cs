using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneAPI.Models
{
    /// <summary>
    /// Load drone with medications model
    /// </summary>
    public class LoadDroneWithMedicationsModel
    {
        public LoadDroneWithMedicationsModel() { MedicationIds = new List<int>(); }

        /// <summary>
        /// Gets or sets the medication ids.
        /// </summary>
        public IList<int> MedicationIds { get; set; }

    }
}
