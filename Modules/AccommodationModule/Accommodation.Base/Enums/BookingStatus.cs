namespace Accommodation.Base.Enums
{
    /// <summary>
    /// Represents the various statuses that a booking can have throughout its lifecycle.
    /// </summary>
    public enum BookingStatus
    {
        /// <summary>
        /// The booking has been created but is yet to be confirmed or processed.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// The booking is currently active and the reservation is in effect.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The booking has been completed successfully, and all services have been rendered.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// The booking has been cancelled by the user or the system before completion.
        /// </summary>
        Cancelled = 3,

        /// <summary>
        /// The booking requires manual confirmation, typically involving additional verification or approval steps.
        /// </summary>
        ManualConformation = 4,
    }
}