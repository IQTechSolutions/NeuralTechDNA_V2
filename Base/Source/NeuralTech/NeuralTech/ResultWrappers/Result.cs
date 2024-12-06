using NeuralTech.Interfaces;

namespace NeuralTech.ResultWrappers
{
    /// <summary>
    /// Represents the result of an operation, including success status and messages.
    /// </summary>
    public class Result : IBaseResult
    {
        /// <summary>
        /// Gets or sets the messages associated with the result.
        /// </summary>
        public List<string> Messages { get; set; } = new();

        /// <summary>
        /// Gets or sets a value indicating whether the operation succeeded.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Creates a failed result with a single message.
        /// </summary>
        /// <param name="message">The failure message.</param>
        /// <returns>A <see cref="Result"/> indicating failure.</returns>
        public static Result Fail(string message) => Create(false, new List<string> { message });

        /// <summary>
        /// Creates a failed result with multiple messages.
        /// </summary>
        /// <param name="messages">The list of failure messages.</param>
        /// <returns>A <see cref="Result"/> indicating failure.</returns>
        public static Result Fail(List<string> messages) => Create(false, messages);

        /// <summary>
        /// Creates a successful result with a single message.
        /// </summary>
        /// <param name="message">The success message.</param>
        /// <returns>A <see cref="Result"/> indicating success.</returns>
        public static Result Success(string message) => Create(true, new List<string> { message });

        /// <summary>
        /// Creates a successful result with no messages.
        /// </summary>
        /// <returns>A <see cref="Result"/> indicating success.</returns>
        public static Result Success() => Create(true);

        /// <summary>
        /// Creates a result with the specified success status and messages.
        /// </summary>
        /// <param name="succeeded">A value indicating whether the operation succeeded.</param>
        /// <param name="messages">The list of messages associated with the result.</param>
        /// <returns>A <see cref="Result"/> with the specified success status and messages.</returns>
        private static Result Create(bool succeeded, List<string>? messages = null)
        {
            return new Result
            {
                Succeeded = succeeded,
                Messages = messages ?? new List<string>()
            };
        }

        /// <summary>
        /// Asynchronously creates a successful result with no messages.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a <see cref="Result"/> indicating success.</returns>
        public static Task<Result> SuccessAsync() => Task.FromResult(Success());

        /// <summary>
        /// Asynchronously creates a failed result with a single message.
        /// </summary>
        /// <param name="message">The failure message.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="Result"/> indicating failure.</returns>
        public static Task<Result> FailAsync(string message) => Task.FromResult(Fail(message));
    }

    /// <summary>
    /// Represents the result of an operation with a data payload, including success status and messages.
    /// </summary>
    /// <typeparam name="T">The type of the data payload.</typeparam>
    public class Result<T> : Result, IBaseResult<T>
    {
        /// <summary>
        /// Gets or sets the data payload of the result.
        /// </summary>
        public T? Data { get; set; } = default;

        /// <summary>
        /// Creates a failed result with a single message.
        /// </summary>
        /// <param name="message">The failure message.</param>
        /// <returns>A <see cref="Result{T}"/> indicating failure.</returns>
        public new static Result<T> Fail(string message) => Create(false, default, new List<string> { message });

        /// <summary>
        /// Creates a failed result with multiple messages.
        /// </summary>
        /// <param name="messages">The list of failure messages.</param>
        /// <returns>A <see cref="Result{T}"/> indicating failure.</returns>
        public new static Result<T> Fail(List<string> messages) => Create(false, default, messages);

        /// <summary>
        /// Creates a successful result with a data payload.
        /// </summary>
        /// <param name="data">The data payload.</param>
        /// <returns>A <see cref="Result{T}"/> indicating success.</returns>
        public static Result<T> Success(T data) => Create(true, data);

        /// <summary>
        /// Creates a successful result with a data payload and a single message.
        /// </summary>
        /// <param name="data">The data payload.</param>
        /// <param name="message">The success message.</param>
        /// <returns>A <see cref="Result{T}"/> indicating success.</returns>
        public static Result<T> Success(T data, string message) => Create(true, data, new List<string> { message });

        /// <summary>
        /// Creates a result with the specified success status, data payload, and messages.
        /// </summary>
        /// <param name="succeeded">A value indicating whether the operation succeeded.</param>
        /// <param name="data">The data payload.</param>
        /// <param name="messages">The list of messages associated with the result.</param>
        /// <returns>A <see cref="Result{T}"/> with the specified success status, data payload, and messages.</returns>
        private static Result<T> Create(bool succeeded, T? data = default, List<string>? messages = null)
        {
            return new Result<T>
            {
                Succeeded = succeeded,
                Data = data,
                Messages = messages ?? new List<string>()
            };
        }

        /// <summary>
        /// Asynchronously creates a successful result with a data payload.
        /// </summary>
        /// <param name="data">The data payload.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="Result{T}"/> indicating success.</returns>
        public static Task<Result<T>> SuccessAsync(T data) => Task.FromResult(Success(data));

        /// <summary>
        /// Asynchronously creates a failed result with a single message.
        /// </summary>
        /// <param name="message">The failure message.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="Result{T}"/> indicating failure.</returns>
        public new static Task<Result<T>> FailAsync(string message) => Task.FromResult(Fail(message));
    }
}
