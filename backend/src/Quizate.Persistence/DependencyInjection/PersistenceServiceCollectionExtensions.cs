using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Quizate.Persistence.DependencyInjection;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddDbContext<QuizateDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DatabaseConnection"))
                .UseSnakeCaseNamingConvention());

        return services;
    }
}
