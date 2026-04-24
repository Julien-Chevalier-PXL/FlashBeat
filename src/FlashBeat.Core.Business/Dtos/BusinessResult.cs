namespace FlashBeat.Core.Business.Dtos;

/// <summary>
/// Record representing the result of a business operation.
/// </summary>
public record BusinessResult
{
    private protected BusinessResult() { }

    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Gets the error message if the operation failed.
    /// </summary>
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// Creates a new successful business result istance.
    /// </summary>
    /// <returns>A successfull <see cref="BusinessResult"/> instance.</returns>
    public static BusinessResult Success() => new() { IsSuccess = true };

    /// <summary>
    /// Creates a failed business result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <returns>
    /// A <see cref="BusinessResult"/> instance representing a failed operation with the provided error message.
    /// </returns>
    public static BusinessResult Error(string errorMessage) => new() { ErrorMessage = errorMessage };
}

/// <summary>
/// Record representing the result of a business operation that returns a value.
/// </summary>
public sealed record BusinessResult<TResult> : BusinessResult
{
    private BusinessResult() { }

    /// <summary>
    /// Gets the result of the operation if it completed successfully.
    /// </summary>
    public TResult? Result { get; init; }

    /// <summary>
    /// Creates a new successful business result istance containing the specified value.
    /// </summary>
    /// <param name="result">The value to include in the successful result.</param>
    /// <returns>
    /// A <see cref="BusinessResult{TResult}"/> instance representing a successful operation with the provided result
    /// value.
    /// </returns>
    public static BusinessResult<TResult> Success(TResult result) => new() { IsSuccess = true, Result = result };

    /// <summary>
    /// Creates a failed business result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <returns>
    /// A <see cref="BusinessResult"/> instance representing a failed operation with the provided error message.
    /// </returns>
    public static new BusinessResult<TResult> Error(string errorMessage) => new() { ErrorMessage = errorMessage };
}
