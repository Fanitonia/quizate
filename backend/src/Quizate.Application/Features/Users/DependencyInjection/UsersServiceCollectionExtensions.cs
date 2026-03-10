using Microsoft.Extensions.DependencyInjection;
using Quizate.Application.Features.Users.Interfaces;
using Quizate.Application.Features.Users.Services;

namespace Quizate.Application.Features.Users.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}

