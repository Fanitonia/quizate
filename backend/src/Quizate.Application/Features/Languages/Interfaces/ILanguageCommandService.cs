using Quizate.Application.Common.Result;
using Quizate.Application.Features.Languages.DTOs.Requests;

namespace Quizate.Application.Features.Languages.Interfaces;

public interface ILanguageCommandService
{
    public Task<Result> CreateAsync(CreateLanguageRequest request);
    public Task<Result> DeleteAsync(string code);
}
