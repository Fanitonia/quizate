using Microsoft.Extensions.DependencyInjection;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Application.Features.Quizzes.Services;

namespace Quizate.Application.Features.Quizzes.DependencyInjection;

internal static class QuizzesServiceCollectionExtensions
{
    public static IServiceCollection AddQuizServices(this IServiceCollection service)
    {
        service.AddScoped<IQuizQueryService, QuizQueryService>();
        service.AddScoped<IQuizCommandService, QuizCommandService>();
        service.AddScoped<IQuizAuthorizationService, QuizAuthorizationService>();

        return service;
    }
}
