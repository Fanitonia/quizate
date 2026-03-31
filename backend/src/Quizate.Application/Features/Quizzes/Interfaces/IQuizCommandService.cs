using Quizate.Application.Common.Result;
using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;

namespace Quizate.Application.Features.Quizzes.Interfaces;

public interface IQuizCommandService
{
    public Task<Result> DeleteQuizAsync(Guid quizId);
    public Task<Result<QuizResponse?>> CreateQuizAsync(CreateQuizRequest request, Guid? userId);
    public Task<Result> UpdateQuizAsync(UpdateQuizRequest request, Guid quizId);
}
