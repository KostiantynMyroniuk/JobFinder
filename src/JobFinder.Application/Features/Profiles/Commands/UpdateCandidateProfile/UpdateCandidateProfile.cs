using AutoMapper;
using JobFinder.Application.Interfaces;
using JobFinder.Domain.Entities;
using JobFinder.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Profiles.Commands.UpdateCandidateProfile
{
    public record UpdateCandidateProfileCommand(
        Guid UserId,
        string FirstNameAndLastName,
        string Position,
        int SalaryExpectation,
        int Experience,
        WorkFormat[]? WorkFormats,
        string? Country,
        string? City,
        string? ExperienceDescription
        ) : IRequest<Guid>;

    public class UpdateCandidateProfileCommandHandler : IRequestHandler<UpdateCandidateProfileCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCandidateProfileCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateCandidateProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _context.CandidateProfiles
                .FirstOrDefaultAsync(p => p.UserId == request.UserId, cancellationToken);

            if (profile == null)
            {
                throw new KeyNotFoundException($"Profile for user {request.UserId} not found.");
            }

            _mapper.Map(request, profile);

            await _context.SaveChangesAsync(cancellationToken);

            return profile.Id;
        }
    }
}
