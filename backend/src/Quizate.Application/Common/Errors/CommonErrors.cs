using Quizate.Application.Common.Result;

namespace Quizate.Application.Common.Errors;

public static class CommonErrors
{
    public static Error NoChangesWereMade => new("NO_CHANGES", "No changes were made.");
    public static Error NotFound(string entityName, string entityId) => new("NOT_FOUND", $"{entityName} with ID '{entityId}' not found.");
}
