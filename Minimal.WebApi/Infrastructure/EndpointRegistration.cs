using System.Reflection;
using System.Runtime.CompilerServices;
using Light.GuardClauses;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Minimal.WebApi.Infrastructure;

public static class EndpointRegistration
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static IServiceCollection AddEndpoints(this IServiceCollection services,
                                                  Assembly[]? targetAssemblies = null)
    {
        if (targetAssemblies.IsNullOrEmpty())
            targetAssemblies = new[] { Assembly.GetCallingAssembly() };

        var endpointInterface = typeof(IEndpoint);
        foreach (var targetAssembly in targetAssemblies)
        {
            foreach (var type in targetAssembly.ExportedTypes)
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                if (type.Implements(endpointInterface))
                    services.AddSingleton(endpointInterface, type);
            }
        }

        return services;
    }

    public static WebApplication UseEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetServices<IEndpoint>();
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoints(app);
        }

        return app;
    }
}