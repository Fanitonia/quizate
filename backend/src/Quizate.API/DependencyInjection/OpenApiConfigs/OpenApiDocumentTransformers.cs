using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Quizate.API.DependencyInjection.OpenApiConfigs;

internal class OpenApiDocumentTransformer(IHostEnvironment environment) : IOpenApiDocumentTransformer
{
    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["CookieAuth"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                In = ParameterLocation.Cookie,
                Name = "ACCESS_TOKEN",
                Description = "JWT is stored in HttpOnly cookie set by /auth/login, /auth/register and /auth/refreshToken."
            }
        };

        if (environment.IsProduction())
        {
            document.Servers =
            [
                new OpenApiServer
                {
                    Url = "https://api.quizate.com"
                }
            ];
        }

        return Task.CompletedTask;
    }
}