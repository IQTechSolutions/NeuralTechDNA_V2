using NeuralTech.Extensions;

namespace NeuralTech.Helpers
{
    /// <summary>
    /// Provides extension methods for building API client paths.
    /// </summary>
    public static class ApiRouteBuilder
    {
        /// <summary>
        /// Combines the base path with a method route to create a full client path.
        /// </summary>
        /// <param name="basePath">The base path of the API.</param>
        /// <param name="methodRoute">The specific method route to append to the base path.</param>
        /// <returns>The full client path as a string.</returns>
        public static string GetClientPath(this string basePath, string methodRoute) => $"{basePath}/{methodRoute}";

        /// <summary>
        /// Combines the base path with a method route and formats it with the provided arguments to create a full client path.
        /// </summary>
        /// <param name="basePath">The base path of the API.</param>
        /// <param name="methodRoute">The specific method route to append to the base path.</param>
        /// <param name="args">The arguments to format the method route with.</param>
        /// <returns>The full client path as a string.</returns>
        public static string GetClientPath(this string basePath, string methodRoute, params string?[] args)
        {
            return $"{basePath}/{string.Format(methodRoute, args)}";
        }

        /// <summary>
        /// Combines the base path with a method route and appends a query string to create a full client path.
        /// </summary>
        /// <param name="basePath">The base path of the API.</param>
        /// <param name="methodRoute">The specific method route to append to the base path.</param>
        /// <param name="queryString">The query string to append to the method route.</param>
        /// <returns>The full client path as a string.</returns>
        public static string GetClientPath(this string basePath, string methodRoute, object queryString) => $"{basePath}/{methodRoute}?{queryString.ToQueryString()}";
    }
}

