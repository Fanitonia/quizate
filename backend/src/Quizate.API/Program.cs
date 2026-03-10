using FluentValidation;
using Quizate.API.Configurations;
using Quizate.API.Extensions;
using Quizate.Application.DependencyInjection;
using Quizate.Persistence.DependencyInjection;
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

            builder.Services.AddProblemDetails();

            builder.Services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer<CookieSecurityDocumentTransformer>();
                options.AddOperationTransformer<CookieSecurityOperationTransformer>();
            });

            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddApplicationServices(builder.Configuration);


            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "v1");
                    options.RoutePrefix = string.Empty;
                    options.EnablePersistAuthorization();
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
