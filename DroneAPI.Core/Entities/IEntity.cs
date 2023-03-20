using System;
using System.ComponentModel.DataAnnotations;

namespace DroneAPI.Core
{
    /// <summary>
    /// Entity model.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [StringLength(128)]
        string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        [Required]
        DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        [StringLength(128)]
        string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date time.
        /// </summary>
        DateTime? ModifiedDateTime { get; set; }
    }
}
