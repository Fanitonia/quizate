namespace Quizate.API.Services.Auth
{
    // IDEA: bunu sadece auth için değilde genel result classı yapabiliriz.
    public class AuthResult
    {
        public bool Success { get; private set; }
        public IReadOnlyList<string> Errors { get; private set; }

        protected AuthResult(bool success, IReadOnlyList<string> errors)
        {
            Success = success;
            Errors = errors;
        }

        public static AuthResult Ok() => new(true, []);
        public static AuthResult Fail(string[] errors) => new(false, errors);
    }

    public class AuthResult<T> : AuthResult
    {
        public T? Data { get; private set; }
        private AuthResult(bool success, IReadOnlyList<string> errors, T? data = default) : base(success, errors)
        {
            Data = data;
        }

        public static AuthResult<T> Ok(T data) => new(true, [], data);
        public static new AuthResult<T> Fail(string[] errors) => new(false, errors);
    }
}
