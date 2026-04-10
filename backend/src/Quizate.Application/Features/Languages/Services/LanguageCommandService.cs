using Microsoft.EntityFrameworkCore;
using Quizate.Application.Common.Errors;
using Quizate.Application.Common.Result;
using Quizate.Application.Features.Languages.DTOs.Requests;
using Quizate.Application.Features.Languages.Errors;
using Quizate.Application.Features.Languages.Interfaces;
using Quizate.Domain.Entities.Quizzes;
using Quizate.Persistence;

namespace Quizate.Application.Features.Languages.Services;

public class LanguageCommandService(QuizateDbContext context) : ILanguageCommandService
{
    public async Task<Result> CreateAsync(CreateLanguageRequest request)
    {
        if (await context.QuizLanguages.AnyAsync(l => l.Code == request.Code))
        {
            return Result.Failure(LanguageErrors.LanguageExist);
        }

        context.QuizLanguages.Add(new QuizLanguage(request.Code));
        var result = await context.SaveChangesAsync();
        return result > 0 ? Result.Success() : Result.Failure(CommonErrors.CreateFailed);
    }

    public async Task<Result> DeleteAsync(string code)
    {
        var language = await context.QuizLanguages.FindAsync(code);
        if (language == null)
            return Result.Failure(CommonErrors.NotFound);

        context.QuizLanguages.Remove(language);
        var result = await context.SaveChangesAsync();
        return result > 0 ? Result.Success() : Result.Failure(CommonErrors.DeleteFailed);
    }
}
