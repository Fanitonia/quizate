using Microsoft.Extensions.DependencyInjection;
using Quizate.Application.Features.Topics.Interfaces;
using Quizate.Application.Features.Topics.Services;

namespace Quizate.Application.Features.Topics.DependencyInjection;

internal static class TopicDependencyInjection
{
    public static void AddTopicServices(this IServiceCollection services)
    {
        services.AddScoped<ITopicCommandService, TopicCommandService>();
        services.AddScoped<ITopicQueryService, TopicQueryService>();
    }
}
