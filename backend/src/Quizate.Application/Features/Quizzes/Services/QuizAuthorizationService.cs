using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Persistence;

namespace Quizate.Application.Features.Quizzes.Services;

public class QuizAuthorizationService(QuizateDbContext context) : IQuizAuthorizationService
{
    public async Task<bool> IsUserQuizOwner(Guid userId, Guid quizId)
    {
        var quiz = await context.Quizzes.FindAsync(quizId);

        if (quiz == null || quiz.CreatorId != userId)
            return false;

        return true;
    }
}
