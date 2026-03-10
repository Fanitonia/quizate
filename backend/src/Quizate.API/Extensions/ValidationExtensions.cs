using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Quizate.API.Extensions;

public static class ValidationExtensions
{
    public static void AddErrorsToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}
