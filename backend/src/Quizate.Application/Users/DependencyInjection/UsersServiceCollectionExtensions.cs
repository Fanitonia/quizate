using Microsoft.Extensions.DependencyInjection;
using Quizate.Application.Users.Interfaces;
using Quizate.Application.Users.Services;

namespace Quizate.Application.Users.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}

