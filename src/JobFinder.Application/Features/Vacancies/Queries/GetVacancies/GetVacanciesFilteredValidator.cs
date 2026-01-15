using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Vacancies.Queries.GetVacancies
{
    public class GetVacanciesFilteredValidator : AbstractValidator<GetVacanciesFilteredQuery>
    {
        public GetVacanciesFilteredValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than {ComparisonValue}.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between {From} and {To} elements.");

            RuleForEach(x => x.WorkFormats)
                .IsInEnum().WithMessage("One or more work formats are invalid.");
        }
    }
}
