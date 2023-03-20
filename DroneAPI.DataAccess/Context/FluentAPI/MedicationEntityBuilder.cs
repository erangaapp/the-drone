using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneAPI.Data.Context.FluentAPI
{
    /// <summary>
    /// The Medication Entity Builder
    /// </summary>
    public class MedicationEntityBuilder
    {
        /// <summary>
        /// Builds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Build(EntityTypeBuilder<MedicationEntity> entity)
            => entity.HasBase();
    }
}
