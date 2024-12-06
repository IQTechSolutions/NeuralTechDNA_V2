using NeuralTech.Entities;
using NeuralTech.Enums;
using System.ComponentModel.DataAnnotations;

namespace NeuralTech.EntityFramework.Entities
{
    /// <summary>
    /// Represents an audit trail entry.
    /// </summary>
    public class Audit : EntityBase<string>
    {
        /// <summary>
        /// Gets or sets the identifier of the user who performed the action.
        /// This may be null if the action was performed by the system.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the type of action performed (Insert, Update, Delete).
        /// Using an enum ensures type safety and restricts values to predefined options.
        /// </summary>
        [Required]
        public AuditActionType ActionType { get; set; }

        /// <summary>
        /// Gets or sets the name of the database table affected by the operation.
        /// This property is required and cannot be null or empty.
        /// </summary>
        [Required]
        public string TableName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the timestamp when the audit event occurred.
        /// Defaults to the current UTC time to maintain consistency across different time zones.
        /// </summary>
        public DateTime EventTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the JSON representation of the entity's state before the operation.
        /// This may be null for insert operations where no prior state exists.
        /// </summary>
        public string? OldValues { get; set; }

        /// <summary>
        /// Gets or sets the JSON representation of the entity's state after the operation.
        /// This may be null for delete operations where the entity no longer exists.
        /// </summary>
        public string? NewValues { get; set; }

        /// <summary>
        /// Gets or sets a list of columns that were affected during the operation.
        /// Stored as a comma-separated string. May be null if no specific columns are tracked.
        /// </summary>
        public string? AffectedColumns { get; set; }

        /// <summary>
        /// Gets or sets the primary key value(s) of the affected record.
        /// For composite keys, values should be concatenated or serialized appropriately.
        /// This property is required.
        /// </summary>
        [Required]
        public string PrimaryKey { get; set; } = string.Empty;
    }
}
