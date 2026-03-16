using Quizate.Application.Common.Result;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Persistence;

namespace Quizate.Application.Features.Quizzes.Services;

public class QuizCommandService(QuizateDbContext context) : IQuizCommandService
{
    public async Task<Result> DeleteQuizAsync(Guid quizId)
    {
        var quiz = await context.Quizzes.FindAsync(quizId);

        if (quiz == null)
            return Result.Failure("Quiz not found.");

        context.Quizzes.Remove(quiz);
        context.SaveChanges();

        return Result.Success();
    }
}
