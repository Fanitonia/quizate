using Microsoft.EntityFrameworkCore;
using Quizate.API.Data;
using Serilog;

namespace Quizate.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();


        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSerilog();

        builder.Services.AddControllers();

        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<QuizateDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection"))
            .UseSnakeCaseNamingConvention());


        var app = builder.Build();


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

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

        app.Run();
    }
}
