using AutoMapper;
using JobFinder.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Vacancies.Queries.GetVacancyById
{
    public record GetVacancyByIdQuery(int Id) : IRequest<VacancyDto>;

    public class GetVacancyById : IRequestHandler<GetVacancyByIdQuery, VacancyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetVacancyById(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacancyDto> Handle(GetVacancyByIdQuery request, CancellationToken cancellationToken)
        {
            var vacancy = await _context.Vacancies.FindAsync(request.Id, cancellationToken);

            if (vacancy is null)
                throw new KeyNotFoundException("Vacancy not found.");

            var vacancyDto = _mapper.Map<VacancyDto>(vacancy);

            return vacancyDto;
        }
    }
}
