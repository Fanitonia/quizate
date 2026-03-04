using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Quizate.API.Services.Auth;

public static class AuthResultExtensions
{
    public static AuthResult<T> AddErrorsToModelState<T>(this AuthResult<T> authResult,
        ModelStateDictionary modelState,
        string key)
    {
        foreach (string error in authResult.Errors)
        {
            modelState.AddModelError(key, error);
        }

        return authResult;
    }

    public static AuthResult AddErrorsToModelState(this AuthResult authResult,
        ModelStateDictionary modelState,
        string key)
    {
        foreach (string error in authResult.Errors)
        {
            modelState.AddModelError(key, error);
        }

        return authResult;
    }
}
