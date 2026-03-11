using FluentValidation;
using Quizate.API.DependencyInjection.OpenApi;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Quizate.API.DependencyInjection;

internal static class APIDependencyInjectionExtension
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        services.AddSerilog();

        services.AddControllers();

        services.AddAuthorization();

        services.AddProblemDetails();

        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddFluentValidationAutoValidation();

        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<OpenApiDocumentTransformer>();
            options.AddOperationTransformer<OpenApiOperationTransformer>();
        });

        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyMethod()
                      .AllowAnyHeader()
                      .WithOrigins(new[] { "https://api.quizate.com", "https://quizate.com" });
            });
        });

        return services;
    }
}
