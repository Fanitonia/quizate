using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Quizate.API.Shared.Result;

public static class ResultExtensions
{
    public static Result<T> AddErrorsToModelState<T>(this Result<T> authResult,
        ModelStateDictionary modelState,
        string key)
    {
        foreach (string error in authResult.Errors)
        {
            modelState.AddModelError(key, error);
        }

        return authResult;
    }

    public static Result AddErrorsToModelState(this Result authResult,
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
