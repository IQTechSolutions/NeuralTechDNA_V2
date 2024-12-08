using System.ComponentModel.DataAnnotations;

namespace NeuralTech.Extensions
{
    /// <summary>
    /// Extension methods for enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the display name for an enum value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The display name of the enum value.</returns>
        public static string GetDisplayName<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attributes.Length > 0)
                {
                    return ((DisplayAttribute)attributes[0]).Name ?? enumValue.ToString();
                }
            }
            return enumValue.ToString();
        }
    }
}
