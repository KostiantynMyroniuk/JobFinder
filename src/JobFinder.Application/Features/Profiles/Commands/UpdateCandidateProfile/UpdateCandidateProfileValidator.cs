using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Profiles.Commands.UpdateCandidateProfile
{
    public class UpdateCandidateProfileValidator : AbstractValidator<UpdateCandidateProfileCommand>
    {
        public UpdateCandidateProfileValidator()
        {
            RuleForEach(x => x.WorkFormats)
                .IsInEnum().WithMessage("Invalid work formats.");

            RuleFor(x => x.ExperienceDescription)
                .MinimumLength(300).WithMessage("Description must be at least 300 characters long.");
        }
    }
}
