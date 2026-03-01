using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quizate.API.Data;
using Quizate.API.Services;
using Quizate.API.Startup;
using Quizate.Data.Models;
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

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSerilog();

            builder.Services.AddControllers();

            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<QuizateDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection"))
                .UseSnakeCaseNamingConvention());

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddProblemDetails();

            builder.Services.AddOpenApi();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            });

            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
            else
                app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            await app.MigrateDatabaseAsync();


            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}
