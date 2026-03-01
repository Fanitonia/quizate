using Microsoft.EntityFrameworkCore;
using Quizate.API.Data;
using Serilog;

namespace Quizate.API.Startup
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<QuizateDbContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, "Something gone wrong while migrating or creating the database");
            }

            return app;
        }
    }
}
