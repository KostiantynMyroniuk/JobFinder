using JobFinder.Application.Events;
using JobFinder.Application.Interfaces;
using JobFinder.Domain.Entities;
using JobFinder.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Profiles.Commands.CreateCandidateProfile
{
    public class CreateProfileEventHandler : INotificationHandler<UserRegisteredEvent>
    {
        private readonly IApplicationDbContext _context;

        public CreateProfileEventHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            if (notification.UserType == UserType.Candidate)
                _context.CandidateProfiles.Add(new CandidateProfile { UserId = notification.Id });
            else if (notification.UserType == UserType.Employer)
                _context.CompanyProfiles.Add(new CompanyProfile { UserId = notification.Id });

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
