using FluentValidation;
using MeteoriteAPI.Common.Models.Filters;

namespace MeteoriteAPI.Web.Validation
{
    public class MeteoriteFilterValidator : AbstractValidator<MeteoriteFilter>
    {
        public MeteoriteFilterValidator()
        {
            RuleFor(_ => _.Year)
                .NotNull()
                .SetValidator(new YearFilterValidator());

            RuleFor(_ => _.Sorting)
                .SetValidator(new SortingValidator())
                .When(_ => _.Sorting is not null);
        }
    }
}
