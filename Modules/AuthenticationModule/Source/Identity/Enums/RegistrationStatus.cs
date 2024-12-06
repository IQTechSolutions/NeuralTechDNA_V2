namespace Identity.Enums
{
    /// <summary>
    /// Enumeration representing the status of a registration process.
    /// </summary>
    public enum RegistrationStatus
    {
        /// <summary>
        /// Registration is pending and awaiting approval.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Registration has been rejected.
        /// </summary>
        Rejected = 1,

        /// <summary>
        /// Registration has been accepted.
        /// </summary>
        Accepted = 2
    }
}