using AutoMapper;
using JobFinder.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Profiles.Queries.GetCandidateProfile
{
    public record GetCandidateProfileQuery(
        Guid Id
        ) : IRequest<CandidateProfileDto>;

    public class GetCandidateProfile : IRequestHandler<GetCandidateProfileQuery, CandidateProfileDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCandidateProfile(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CandidateProfileDto> Handle(GetCandidateProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await _context.CandidateProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            return _mapper.Map<CandidateProfileDto>(profile);

        }
    }
}
