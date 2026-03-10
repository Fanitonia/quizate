using FluentValidation;
using Quizate.API.Extensions;
using Quizate.API.OpenApi.DependencyInjection;
using Quizate.Application.DependencyInjection;
using Quizate.Persistence.DependencyInjection;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

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

            builder.Services.AddOpenApiWithDefinedOptions();

            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddFluentValidationAutoValidation();

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
