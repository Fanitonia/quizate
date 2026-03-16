using Quizate.Application.Common.Result;

namespace Quizate.Application.Features.Quizzes.Interfaces;

public interface IQuizCommandService
{
    public Task<Result> DeleteQuizAsync(Guid quizId);
}
