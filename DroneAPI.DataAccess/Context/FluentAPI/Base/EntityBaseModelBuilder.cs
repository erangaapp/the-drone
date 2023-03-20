using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneAPI.Data
{
    /// <summary>
    /// Entity base model builder
    /// </summary>
    public static class EntityBaseModelBuilder
    {
        /// <summary>
        /// Determines whether this instance has base.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public static void HasBase<T>(this EntityTypeBuilder<T> entity) where T : EntityBase
        {
            #region PrimaryKey
            entity.HasKey(t => t.Id);
            #endregion

            #region Properties
            entity.Property(t => t.CreatedDateTime)
              .IsRequired();
            entity.Property(t => t.CreatedBy)
            .IsRequired();
            #endregion
        }
    }
}
