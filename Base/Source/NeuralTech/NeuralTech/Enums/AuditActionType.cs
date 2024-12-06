namespace NeuralTech.Enums
{
    /// <summary>
    /// Enumeration representing the types of audit actions that can be performed on an entity.
    /// </summary>
    public enum AuditActionType : byte
    {
        /// <summary>
        /// No audit action.
        /// </summary>
        None = 0,

        /// <summary>
        /// Audit action for creating an entity.
        /// </summary>
        Create = 1,

        /// <summary>
        /// Audit action for updating an entity.
        /// </summary>
        Update = 2,

        /// <summary>
        /// Audit action for deleting an entity.
        /// </summary>
        Delete = 3
    }
}
