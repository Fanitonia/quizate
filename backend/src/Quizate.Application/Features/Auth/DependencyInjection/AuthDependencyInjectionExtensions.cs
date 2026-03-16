using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quizate.Application.Features.Auth.Interfaces;
using Quizate.Application.Features.Auth.Services;
using Quizate.Domain.Entities.Users;

namespace Quizate.Application.Features.Auth.DependencyInjection;

internal static class AuthDependencyInjectionExtensions
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}
