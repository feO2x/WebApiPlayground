using Microsoft.AspNetCore.Mvc;
using Mvc.WebApi.Infrastructure;

namespace Mvc.WebApi;

[ApiController]
public sealed class HomeController : ControllerBase
{
    public HomeController(PagingValidator validator) => Validator = validator;

    private PagingValidator Validator { get; }

    [HttpGet("/api/hohoho")]
    public ActionResult<HomeResult> GetHoHoHo(int skip = 0, int take = 30)
    {
        if (this.CheckPagingParametersForErrors(skip, take))
            return ValidationProblem();

        return new HomeResult("Ho Ho Ho!");
    }

    [HttpGet("/api/hihihi")]
    public ActionResult<HomeResult> GetHihihi(int skip = 0, int take = 30)
    {
        if (this.CheckPagingParametersForErrors(skip, take))
            return BadRequest(ModelState);

        return new HomeResult("Hi Hi Hi!");
    }

    [HttpGet("/api/hahaha")]
    public ActionResult<HomeResult> GetHahaha(int skip = 0, int take = 30)
    {
        if (this.CheckPagingInfoForErrors(Validator, new PagingInfo(skip, take)))
            return BadRequest(ModelState);

        return new HomeResult("Ha Ha Ha!");
    }
}

public sealed record HomeResult(string Message);