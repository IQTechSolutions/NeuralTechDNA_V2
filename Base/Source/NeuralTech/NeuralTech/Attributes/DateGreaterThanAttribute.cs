using System.ComponentModel.DataAnnotations;

namespace NeuralTech.Attributes
{
    /// <summary>
    /// Specifies that a date property must be greater than another date property in the same class.
    /// </summary>
    /// <remarks>
    /// This attribute is used to enforce that one date property occurs after another date property.
    /// For example, ensuring that an <see cref="EndDate"/> is later than a <see cref="StartDate"/>.
    /// </remarks>
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the name of the property to compare with.
        /// </summary>
        private readonly string _comparisonProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateGreaterThanAttribute"/> class.
        /// </summary>
        /// <param name="comparisonProperty">
        /// The name of the property to compare the current property's value against.
        /// This property must be of type <see cref="DateTime"/> or <see cref="Nullable{DateTime}"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="comparisonProperty"/> is null or empty.
        /// </exception>
        public DateGreaterThanAttribute(string comparisonProperty)
        {
            if (string.IsNullOrWhiteSpace(comparisonProperty))
            {
                throw new ArgumentNullException(nameof(comparisonProperty), "Comparison property name cannot be null or empty.");
            }

            _comparisonProperty = comparisonProperty;
        }

        /// <summary>
        /// Validates that the value of the current property is greater than the value of the specified comparison property.
        /// </summary>
        /// <param name="value">The value of the current property being validated.</param>
        /// <param name="validationContext">Context information about the validation operation.</param>
        /// <returns>
        /// An instance of <see cref="ValidationResult"/> indicating whether validation succeeded.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the comparison property does not exist or is not of type <see cref="DateTime"/> or <see cref="Nullable{DateTime}"/>.
        /// </exception>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Retrieve the current value as DateTime
            if (value == null)
            {
                // If the current value is null, consider it valid. Use [Required] attribute for null checks.
                return ValidationResult.Success;
            }

            if (!(value is DateTime currentValue))
            {
                throw new ArgumentException("Attribute can only be applied to DateTime properties.");
            }

            // Get the PropertyInfo object for the comparison property
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException($"Property with name '{_comparisonProperty}' not found.");
            }

            // Get the value of the comparison property
            var comparisonValueObj = property.GetValue(validationContext.ObjectInstance);

            if (comparisonValueObj == null)
            {
                // If the comparison property is null, consider the current value valid.
                // Use [Required] attribute on the comparison property if it should not be null.
                return ValidationResult.Success;
            }

            if (!(comparisonValueObj is DateTime comparisonValue))
            {
                throw new ArgumentException("Comparison property must be of type DateTime or Nullable<DateTime>.");
            }

            // Perform the comparison
            if (currentValue <= comparisonValue)
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }

            // Validation succeeded
            return ValidationResult.Success;
        }

        /// <summary>
        /// Formats the error message to be displayed when validation fails.
        /// </summary>
        /// <param name="name">The display name of the property being validated.</param>
        /// <returns>A localized error message string.</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"{name} must be greater than {_comparisonProperty}."
                : ErrorMessage;
        }
    }
}
