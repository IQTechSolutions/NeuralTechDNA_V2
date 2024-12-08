using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NeuralTech.Attributes
{
    /// <summary>
    /// Custom validation attribute to ensure that one property is greater than another property within the same class.
    /// </summary>
    /// <remarks>
    /// This attribute is typically used to enforce that a numerical property (e.g., MaxAge) is greater than another numerical property (e.g., MinAge).
    /// It can be applied to any property of type <see cref="int"/> or <see cref="double"/> that needs to be validated against another property.
    /// </remarks>
    public class GreaterThanAttribute : ValidationAttribute
    {
        /// <summary>
        /// The name of the property to compare with.
        /// </summary>
        private readonly string _comparisonProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanAttribute"/> class.
        /// </summary>
        /// <param name="comparisonProperty">
        /// The name of the property to compare the current property's value against.
        /// This property must be of a comparable numerical type (e.g., <see cref="int"/>, <see cref="double"/>).
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="comparisonProperty"/> is null or empty.
        /// </exception>
        public GreaterThanAttribute(string comparisonProperty)
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
        /// <param name="value">
        /// The value of the current property being validated.
        /// </param>
        /// <param name="validationContext">
        /// Context information about the validation operation, including the object being validated.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ValidationResult"/> indicating whether validation succeeded.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the comparison property does not exist or is not of a comparable numerical type.
        /// </exception>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Ensure the current value is not null
            if (value == null)
            {
                // Consider null values as valid. Use [Required] attribute to enforce non-nullability if needed.
                return ValidationResult.Success;
            }

            // Determine the type of the current property
            Type valueType = value.GetType();

            // Supported types for comparison
            Type[] supportedTypes = { typeof(int), typeof(double), typeof(float), typeof(decimal), typeof(long), typeof(short) };

            // Check if the current property's type is supported
            if (Array.IndexOf(supportedTypes, valueType) < 0)
            {
                throw new ArgumentException($"The GreaterThanAttribute is not supported for type {valueType.Name}. Supported types are: int, double, float, decimal, long, short.");
            }

            // Retrieve the comparison property using reflection
            PropertyInfo? comparisonPropertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (comparisonPropertyInfo == null)
            {
                throw new ArgumentException($"Property with name '{_comparisonProperty}' not found.");
            }

            // Get the value of the comparison property
            object? comparisonValue = comparisonPropertyInfo.GetValue(validationContext.ObjectInstance);

            // Ensure the comparison value is not null
            if (comparisonValue == null)
            {
                // If the comparison property is null, consider the current value as valid.
                // Use [Required] attribute on the comparison property if it should not be null.
                return ValidationResult.Success;
            }

            // Determine the type of the comparison property
            Type comparisonType = comparisonValue.GetType();

            // Check if the comparison property's type is supported
            if (Array.IndexOf(supportedTypes, comparisonType) < 0)
            {
                throw new ArgumentException($"The GreaterThanAttribute is not supported for comparison property type {comparisonType.Name}. Supported types are: int, double, float, decimal, long, short.");
            }

            // Convert both values to decimal for comparison to handle different numerical types
            decimal currentDecimalValue = Convert.ToDecimal(value);
            decimal comparisonDecimalValue = Convert.ToDecimal(comparisonValue);

            // Perform the comparison
            if (currentDecimalValue <= comparisonDecimalValue)
            {
                // Format the error message
                string errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }

            // Validation successful
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
