namespace FlashBeat.Web.Service.Dtos;

/// <summary>
/// Record representing the result of a business operation.
/// </summary>
public record ServiceResult
{
    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Gets the error message if the operation failed.
    /// </summary>
    public string? ErrorMessage { get; init; }
}

/// <summary>
/// Record representing the result of a business operation that returns a value.
/// </summary>
public sealed record ServiceResult<TResult> : ServiceResult
{
    /// <summary>
    /// Gets the result of the operation if it completed successfully.
    /// </summary>
    public TResult? Result { get; init; }
}
