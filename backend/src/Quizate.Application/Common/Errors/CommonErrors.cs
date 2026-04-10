using Quizate.Application.Common.Result;

namespace Quizate.Application.Common.Errors;

public static class CommonErrors
{
    public static Error NotFound => new("NOT_FOUND", "The requested resource was not found.");
    public static Error UpdateRequestEmpty => new("UPDATE_REQUEST_EMPTY", "The update request must contain at least one field to update.");
    public static Error CreateFailed => new("CREATE_FAILED", "Failed to create the resource for unknown reason.");
    public static Error DeleteFailed => new("DELETE_FAILED", "Failed to delete the resource for unknown reason.");
}
