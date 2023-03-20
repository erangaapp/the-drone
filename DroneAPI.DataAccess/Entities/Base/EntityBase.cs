using DroneAPI.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace DroneAPI.Data
{
    /// <summary>
    /// The base entity model.
    /// </summary>
    /// <seealso cref="IEntity" />
    public class EntityBase : IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [Required]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        [Required]
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date time.
        /// </summary>
        public DateTime? ModifiedDateTime { get; set; }
    }
}
