namespace Quizate.Application.Features.Quizzes.Interfaces;

public interface IQuizAuthorizationService
{
    public Task<bool> IsUserQuizOwner(Guid userId, Guid quizId);
}
