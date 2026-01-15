using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Auth.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(30).WithMessage("{PropertyName} cannot exceed {MaxLength} characters.")
                .MinimumLength(8).WithMessage("{PropertyName} must be at least {MinLength} characters long.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress().WithMessage("Invalid email.");
        }
    }
}
