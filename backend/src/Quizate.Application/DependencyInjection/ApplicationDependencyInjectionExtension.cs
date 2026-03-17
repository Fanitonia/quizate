using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quizate.Application.Features.Auth.DependencyInjection;
using Quizate.Application.Features.Quizzes.DependencyInjection;
using Quizate.Application.Features.Topics.DependencyInjection;
using Quizate.Application.Features.Users.DependencyInjection;

namespace Quizate.Application.DependencyInjection;

public static class ApplicationDependencyInjectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthServices(configuration);
        services.AddUserServices();
        services.AddQuizServices();
        services.AddTopicServices();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
        });

        return services;
    }
}
