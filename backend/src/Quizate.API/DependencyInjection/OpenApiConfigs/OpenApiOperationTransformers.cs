using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Quizate.API.DependencyInjection.OpenApiConfigs;

internal class OpenApiOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        var metadata = context.Description.ActionDescriptor.EndpointMetadata;

        var hasAllowAnonymous = metadata.OfType<IAllowAnonymous>().Any();
        var hasAuthorize = metadata.OfType<IAuthorizeData>().Any();

        if (hasAllowAnonymous || !hasAuthorize)
            return Task.CompletedTask;

        operation.Security ??= [];
        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("CookieAuth", context.Document)] = []
        });

        return Task.CompletedTask;
    }
}

