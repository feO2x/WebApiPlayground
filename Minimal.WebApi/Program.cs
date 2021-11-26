using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Minimal.WebApi.Infrastructure;
using Serilog;

namespace Minimal.WebApi;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var logger = Logging.CreateLogger();
        try
        {
            var app = WebApplication.CreateBuilder(args)
                                    .ConfigureBuilder(logger)
                                    .ConfigureApp();

            await app.RunAsync();
            return 0;
        }
        catch (Exception exception)
        {
            logger.Fatal(exception, "Could not run web service");
            return -1;
        }
    }

    private static WebApplication ConfigureBuilder(this WebApplicationBuilder builder, ILogger logger)
    {
        builder.Host
               .UseLightInject()
               .UseSerilog(logger);

        builder.Services
               .AddEndpoints()
               .AddSingleton(new PagingValidator());

        return builder.Build();
    }

    private static WebApplication ConfigureApp(this WebApplication app)
    {
        app.UseSerilogRequestLogging()
           .UseRouting();
        app.UseEndpoints();
        return app;
    }
}