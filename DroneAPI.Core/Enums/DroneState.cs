namespace DroneAPI.Core
{
    /// <summary>
    /// Drone state enum
    /// </summary>
    public enum DroneState
    {
        /// <summary>
        /// The idle state
        /// </summary>
        IDLE = 0,

        /// <summary>
        /// The loading state
        /// </summary>
        LOADING = 1,

        /// <summary>
        /// The loaded state
        /// </summary>
        LOADED = 2,

        /// <summary>
        /// The delivering state
        /// </summary>
        DELIVERING = 3,

        /// <summary>
        /// The delivered state
        /// </summary>
        DELIVERED = 4,

        /// <summary>
        /// The returning state
        /// </summary>
        RETURNING = 5,
    }
}
