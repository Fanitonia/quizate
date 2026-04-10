using Microsoft.Extensions.DependencyInjection;
using Quizate.Application.Features.Languages.Interfaces;
using Quizate.Application.Features.Languages.Services;

namespace Quizate.Application.Features.Languages.DependencyInjection;

public static class LanguagesDependencyInjection
{
    public static void AddLanguagesServices(this IServiceCollection services)
    {
        services.AddScoped<ILanguageQueryService, LanguageQueryService>();
        services.AddScoped<ILanguageCommandService, LanguageCommandService>();
    }
}
