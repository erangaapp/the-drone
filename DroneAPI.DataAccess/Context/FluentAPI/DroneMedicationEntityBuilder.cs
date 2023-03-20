using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneAPI.Data.Context.FluentAPI
{
    /// <summary>
    /// The Drone Medication Entity Builder.
    /// </summary>
    public class DroneMedicationEntityBuilder
    {
        /// <summary>
        /// Builds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Build(EntityTypeBuilder<DroneMedicationEntity> entity)
        {
            entity.HasBase();

            entity.HasOne(drone => drone.Drone).
                WithMany(m => m.DroneMedications).
            HasForeignKey(fk => fk.DroneId);

            entity.HasOne(drone => drone.Medication).
                WithMany(m => m.DroneMedications).
                HasForeignKey(fk => fk.MedicationId);
        }
    }
}
