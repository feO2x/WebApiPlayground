using Microsoft.AspNetCore.Routing;

namespace Minimal.WebApi.Infrastructure;

public interface IEndpoint
{
    void MapEndpoints(IEndpointRouteBuilder app);
}