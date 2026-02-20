namespace PestControlCMS.Core.Results;

/// <summary>
/// Discriminated union result type for operations that may succeed or fail
/// without relying on exceptions for control flow.
/// </summary>
/// <typeparam name="T">The type of the success value.</typeparam>
public sealed class Result<T>
{
    private Result(T value) => (Value, IsSuccess, Error) = (value, true, null);
    private Result(string error) => (Value, IsSuccess, Error) = (default, false, error);

    /// <summary>The wrapped value when the operation succeeded.</summary>
    public T? Value { get; }

    /// <summary><c>true</c> if the operation completed successfully.</summary>
    public bool IsSuccess { get; }

    /// <summary>Human-readable error message when <see cref="IsSuccess"/> is <c>false</c>.</summary>
    public string? Error { get; }

    /// <summary>Creates a successful result wrapping <paramref name="value"/>.</summary>
    public static Result<T> Success(T value) => new(value);

    /// <summary>Creates a failed result with the supplied <paramref name="error"/> message.</summary>
    public static Result<T> Failure(string error) => new(error);
}

/// <summary>Non-generic result for void-style operations.</summary>
public sealed class Result
{
    private Result(bool isSuccess, string? error) => (IsSuccess, Error) = (isSuccess, error);

    /// <inheritdoc cref="Result{T}.IsSuccess"/>
    public bool IsSuccess { get; }

    /// <inheritdoc cref="Result{T}.Error"/>
    public string? Error { get; }

    /// <summary>Creates a successful void result.</summary>
    public static Result Success() => new(true, null);

    /// <summary>Creates a failed void result.</summary>
    public static Result Failure(string error) => new(false, error);
}
