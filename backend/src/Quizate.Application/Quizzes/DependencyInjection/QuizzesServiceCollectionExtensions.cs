using Microsoft.Extensions.DependencyInjection;
using Quizate.Application.Quizzes.Interfaces;
using Quizate.Application.Quizzes.Services;

namespace Quizate.Application.Quizzes.DependencyInjection;

internal static class QuizzesServiceCollectionExtensions
{
    public static IServiceCollection AddQuizServices(this IServiceCollection service)
    {
        service.AddScoped<IQuizService, QuizService>();

        return service;
    }
}
