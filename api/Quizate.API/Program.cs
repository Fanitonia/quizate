using Microsoft.EntityFrameworkCore;
using Quizate.API.Data;
using Serilog;

namespace Quizate.API;

public class Program
{
    public static void Main(string[] args)
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


        app.Run();
    }
}
