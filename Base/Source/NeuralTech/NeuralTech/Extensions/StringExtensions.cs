using System.Web;

namespace NeuralTech.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates a string to a specified maximum length and appends " ..." if truncated.
        /// </summary>
        /// <param name="str">The string to truncate.</param>
        /// <param name="maxLength">The maximum length of the string.</param>
        /// <returns>The truncated string with " ..." appended if it exceeds the maximum length.</returns>
        public static string TruncateLongString(this string? str, int maxLength)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length > maxLength)
                {
                    return str.Substring(0, Math.Min(str.Length, maxLength)) + " ...";
                }
                return str;
            }
            return string.Empty;
        }

        /// <summary>
        /// Converts an object's properties to a query string.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <returns>A query string.</returns>
        public static string ToQueryString<T>(this T obj)
        {
            var properties = from p in obj?.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select $"{HttpUtility.UrlEncode(p.Name)}={HttpUtility.UrlEncode(p.GetValue(obj)?.ToString())}";
            return string.Join("&", properties);
        }
    }
}
