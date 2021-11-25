using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.WebApi.Infrastructure;

public static class Paging
{
    public static bool CheckPagingParametersForErrors(this ControllerBase controller,
                                                      int skip,
                                                      int take)
    {
        if (skip < 0)
            controller.ModelState.AddModelError(nameof(skip), "skip must not be less than 0");
        if (take is < 1 or > 100)
            controller.ModelState.AddModelError(nameof(take), "take must be between 1 and 100");

        return controller.ModelState.ErrorCount != 0;
    }

    public static bool CheckPagingInfoForErrors(this ControllerBase controller,
                                                IValidator<PagingInfo> validator,
                                                PagingInfo pagingInfo)
    {
        var validationResult = validator.Validate(pagingInfo);
        if (!validationResult.IsValid)
            validationResult.AddToModelState(controller.ModelState, string.Empty);
        return !validationResult.IsValid;
    }
}

public sealed class PagingValidator : AbstractValidator<PagingInfo>
{
    public PagingValidator()
    {
        RuleFor(i => i.Skip).GreaterThanOrEqualTo(0);
        RuleFor(i => i.Take).GreaterThan(0).LessThanOrEqualTo(100);
    }
}

public readonly record struct PagingInfo(int Skip, int Take);