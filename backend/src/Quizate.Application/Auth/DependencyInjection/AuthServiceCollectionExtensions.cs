using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quizate.Application.Auth.Interfaces;
using Quizate.Application.Auth.Services;
using Quizate.Core.Entities.Users;
using System.Text;

namespace Quizate.Application.Auth.DependencyInjection;

internal static class AuthServiceCollectionExtensions
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddJwtAuthentication();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICookieService, CookieService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }

    internal static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
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
