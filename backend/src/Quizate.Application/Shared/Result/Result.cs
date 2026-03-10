namespace Quizate.Application.Shared.Result;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public IEnumerable<string> Errors { get; protected set; } = [];

    public static Result Success() => new() { IsSuccess = true };
    public static Result Failure(string[] errors) => new() { IsSuccess = false, Errors = errors };
    public static Result Failure(string error) => new() { IsSuccess = false, Errors = new[] { error } };
}

public class Result<T> : Result
{
    public T? Value { get; private set; }

    public static Result<T> Success(T data) => new() { IsSuccess = true, Value = data };
    public static new Result<T> Failure(string[] errors) => new() { IsSuccess = false, Errors = errors };
    public static new Result<T> Failure(string error) => new() { IsSuccess = false, Errors = new[] { error } };
}
