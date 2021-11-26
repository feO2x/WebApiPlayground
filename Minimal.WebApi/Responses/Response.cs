using Microsoft.AspNetCore.Http;

namespace Minimal.WebApi.Responses;

public static class Response
{
    public static IResult BadRequest(object errors) =>
        new BadRequestResponse(errors);
}