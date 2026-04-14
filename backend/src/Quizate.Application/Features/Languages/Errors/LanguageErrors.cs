using Quizate.Application.Common.Result;

namespace Quizate.Application.Features.Languages.Errors;

public static class LanguageErrors
{
    public static Error LanguageExist => new Error("LANGUAGE_EXIST", "Language already exists");
    public static Error LanguageNotFound(string code) => new Error("LANGUAGE_NOT_FOUND", $"Language with code '{code}' not found.");
    public static Error InvalidLanguage => new Error("INVALID_LANGUAGE", "Invalid language code.");
}
