using Microsoft.Extensions.DependencyInjection;
using Quizate.Application.Auth.DependencyInjection;
using Quizate.Application.Quizzes.DependencyInjection;
using Quizate.Application.Users.DependencyInjection;

namespace Quizate.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAuthServices();
        services.AddUsersServices();
        services.AddQuizServices();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
        });

        return services;
    }
}
