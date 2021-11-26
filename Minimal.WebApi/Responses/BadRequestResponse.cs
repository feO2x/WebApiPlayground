using Microsoft.AspNetCore.Http;
using MinimalApis.Extensions.Results;

namespace Minimal.WebApi.Responses;

public sealed class BadRequestResponse : Json
{
    public BadRequestResponse(object errors) : base(errors) =>
        StatusCode = StatusCodes.Status400BadRequest;
}