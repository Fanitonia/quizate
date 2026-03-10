namespace Quizate.API.OpenApi.DependencyInjection;

internal static class OpenApiDependencyInjectionExtension
{
    public static IServiceCollection AddOpenApiWithDefinedOptions(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<CookieSecurityDocumentTransformer>();
            options.AddOperationTransformer<CookieSecurityOperationTransformer>();
        });

        return services;
    }
}
