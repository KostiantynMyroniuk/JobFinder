using AutoMapper;
using JobFinder.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Profiles.Queries.GetCompanyProfile
{
    public record GetCompanyProfileQuery(
        Guid Id
        ) : IRequest<CompanyProfileDto>;

    public class GetCompanyProfileQueryHandler : IRequestHandler<GetCompanyProfileQuery, CompanyProfileDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompanyProfileQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyProfileDto> Handle(GetCompanyProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await _context.CompanyProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            return _mapper.Map<CompanyProfileDto>(profile);
        }
    }
}
