using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Vacancies.Commands.CreateVacancy
{
    public class CreateVacancyValidator : AbstractValidator<CreateVacancyCommand>
    {
        public CreateVacancyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} cannot exceed {MaxLength} characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(100).WithMessage("{PropertyName} length must be at least {MinLength} characters long.")
                .MaximumLength(4000).WithMessage("{PropertyName} cannot exceed {MaxLength} characters.");

            RuleFor(x => x.EnglishLevel)
                .NotEmpty().WithMessage("English level is required.")
                .IsInEnum().WithMessage("English level is invalid.");

            RuleFor(x => x.WorkFormat)
                .IsInEnum().WithMessage("Work format is invalid.");

            RuleFor(x => x.Experience)
                .InclusiveBetween(0, 10).WithMessage("{PropertyName} must be between {From} and {To} years.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(2).WithMessage("{PropertyName} name is too short.")
                .MaximumLength(100).WithMessage("{PropertyName} name is too long.")
                .Matches(@"^[a-zA-Z\s,\-]+$").WithMessage("{PropertyName} name contains invalid characters.");
        }
    }
}
