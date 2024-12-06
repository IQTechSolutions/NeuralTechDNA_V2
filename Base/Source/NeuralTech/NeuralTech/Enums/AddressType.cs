namespace NeuralTech.Enums
{
    /// <summary>
    /// Enumeration representing the types of addresses that can be associated with an entity.
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// Physical address type, typically used for the actual location of a place or entity.
        /// </summary>
        Physical = 0,

        /// <summary>
        /// Shipping address type, used for delivery purposes.
        /// </summary>
        Shipping = 1,

        /// <summary>
        /// Billing address type, used for invoicing and payment purposes.
        /// </summary>
        Billing = 2
    }
}