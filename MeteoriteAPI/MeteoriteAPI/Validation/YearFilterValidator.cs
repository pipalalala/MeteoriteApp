using FluentValidation;
using MeteoriteAPI.Common.Models.Filters;

namespace MeteoriteAPI.Web.Validation
{
    public class YearFilterValidator : AbstractValidator<YearFilter>
    {
        public YearFilterValidator()
        {
            RuleFor(_ => _.StartDateYear)
                .NotNull()
                .GreaterThanOrEqualTo(0);
            RuleFor(_ => _.EndDateYear)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(_ => _.StartDateYear)
                .Must((filter, startDateYear) => startDateYear <= filter.EndDateYear);
            RuleFor(_ => _.EndDateYear)
                .Must((filter, endDateYear) => endDateYear >= filter.StartDateYear);
        }
    }
}
