using Quizate.Application.Common.Result;

namespace Quizate.Application.Common.Errors;

public static class CommonErrors
{
    public static Error NoChangesWereMade => new("NO_CHANGES", "No changes were made.");
    public static Error NotFound => new("NOT_FOUND", $"The requested resource was not found.");
}
