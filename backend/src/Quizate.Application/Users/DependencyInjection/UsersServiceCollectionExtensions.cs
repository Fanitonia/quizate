using Microsoft.Extensions.DependencyInjection;
using Quizate.Application.Users.Interfaces;
using Quizate.Application.Users.Services;

namespace Quizate.Application.Users.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddUsersServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}

