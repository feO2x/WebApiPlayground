using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Minimal.WebApi.Infrastructure;
using Minimal.WebApi.Responses;
using MinimalApis.Extensions.Results;

namespace Minimal.WebApi;

public sealed class HomeEndpoint : IEndpoint
{
    public HomeEndpoint(PagingValidator validator) => Validator = validator;

    private PagingValidator Validator { get; }

    void IEndpoint.MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/hohoho", GetHohoho);
        app.MapGet("/api/hahaha", GetHahaha);
        app.MapGet("/api/exception", GetException);
    }

    public static IResult GetHohoho(int? skip, int? take)
    {
        skip ??= 0;
        take ??= 30;

        if (Paging.CheckPagingParametersForErrors(skip.Value, take.Value, out var errors))
            return Response.BadRequest(errors);

        return Results.Extensions.Ok(new HomeResult("Hohoho"));
    }

    public IResult GetHahaha(int? skip, int? take)
    {
        skip ??= 0;
        take ??= 30;

        var validationResult = Validator.Validate(new PagingInfo(skip.Value, take.Value));
        if (!validationResult.IsValid)
            return Response.BadRequest(validationResult.Errors);

        return Results.Extensions.Ok(new HomeResult("Hahaha"));
    }

    public static IResult GetException()
    {
        throw new Exception("In your face!");
    }
}

public sealed record HomeResult(string Message);