using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneAPI.Data.Context.FluentAPI
{
    /// <summary>
    /// The Drone Entity Builder.
    /// </summary>
    public class DroneEntityBuilder
    {
        /// <summary>
        /// Builds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Build(EntityTypeBuilder<DroneEntity> entity)
           => entity.HasBase();
    }
}
