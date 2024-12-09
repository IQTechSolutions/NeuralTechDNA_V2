namespace Beneficiary.Shared.DataTransferObjects
{
    /// <summary>
    /// A Data Transfer Object (DTO) representing the Ambassador entity.
    /// Used to expose Ambassador data without exposing the entire domain entity,
    /// ideal for API responses or other external communication layers.
    /// </summary>
    public class AmbassadorDto
    {
        /// <summary>
        /// The unique identifier of the ambassador.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The ambassador's first name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The ambassador's surname.
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// The ambassador's phone number.
        /// </summary>
        public string PhoneNr { get; set; } = string.Empty;

        /// <summary>
        /// The ambassador's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The commission percentage the ambassador earns.
        /// </summary>
        public double CommissionPercentage { get; set; }
    }
}