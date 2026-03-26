using Quizate.API.DependencyInjection;
using Quizate.API.Extensions;
using Quizate.Application.DependencyInjection;
using Quizate.Persistence.DependencyInjection;
using Serilog;

namespace Quizate.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddWebApiServices(builder.Configuration)
                            .AddPersistenceServices(builder.Configuration)
                            .AddApplicationServices(builder.Configuration);

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            // şimdilik böyle
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
                options.RoutePrefix = string.Empty;
                options.EnablePersistAuthorization();
            });


            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            await app.MigrateDatabaseAsync();

            await app.RunAsync();
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
