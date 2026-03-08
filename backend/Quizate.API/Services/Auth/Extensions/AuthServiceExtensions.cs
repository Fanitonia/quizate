using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Quizate.Data.Models;
using System.Text;

namespace Quizate.API.Services.Auth;

public static class AuthServiceExtensions
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddJwtAuthentication();
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<ICookieManager, CookieManager>();
        services.AddScoped<ITokenManager, TokenManager>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Request.Cookies.TryGetValue("ACCESS_TOKEN", out var token);
                        context.Token = token;
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
