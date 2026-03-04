using Microsoft.AspNetCore.Identity;
using Quizate.Data.Models;

namespace Quizate.API.Services.Auth;

public static class AddAuthExtension
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<ICookieManager, CookieManager>();
        services.AddScoped<ITokenManager, TokenManager>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}
