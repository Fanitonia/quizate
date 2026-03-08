namespace Quizate.API.Shared.Result;

public class Result
{
    public bool Success { get; private set; }
    public IReadOnlyList<string> Errors { get; private set; }

    protected Result(bool success, IReadOnlyList<string> errors)
    {
        Success = success;
        Errors = errors;
    }

    public static Result Ok() => new(true, []);
    public static Result Fail(string[] errors) => new(false, errors);
}

public class Result<T> : Result
{
    public T? Data { get; private set; }
    private Result(bool success, IReadOnlyList<string> errors, T? data = default) : base(success, errors)
    {
        Data = data;
    }

    public static Result<T> Ok(T data) => new(true, [], data);
    public static new Result<T> Fail(string[] errors) => new(false, errors);
}
