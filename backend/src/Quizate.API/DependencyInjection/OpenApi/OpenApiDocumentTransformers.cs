using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Quizate.API.DependencyInjection.OpenApi;

internal class CookieSecurityDocumentTransformer : IOpenApiDocumentTransformer
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
                Scheme = "cookie",
                In = ParameterLocation.Cookie,
                Name = "ACCESS_TOKEN",
                Description = "JWT is stored in HttpOnly cookie set by /auth/login and /auth/refreshToken."
            }
        };

        return Task.CompletedTask;
    }
}