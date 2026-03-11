using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Quizate.Persistence.DependencyInjection;

public static class PersistenceDependencyInjectionExtension
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<QuizateDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"))
                .UseSnakeCaseNamingConvention());

        return services;
    }
}
