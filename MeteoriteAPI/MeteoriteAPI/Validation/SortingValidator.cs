using FluentValidation;
using MeteoriteAPI.Common.Models.Filters;

namespace MeteoriteAPI.Web.Validation
{
    public class SortingValidator : AbstractValidator<Sorting>
    {
        public SortingValidator()
        {
            RuleFor(_ => _.SortBy)
                .NotNull()
                .IsInEnum();
            RuleFor(_ => _.SortOrder)
                .NotNull()
                .IsInEnum();
        }
    }
}
