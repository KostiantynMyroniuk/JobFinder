using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobFinder.Application.Interfaces;
using JobFinder.Application.Models;
using JobFinder.Domain.Entities;
using JobFinder.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Vacancies.Queries.GetVacancies
{
    public record GetVacanciesFilteredQuery(
        int[]? ExpLevels,
        string[]? PrimaryKeys,
        WorkFormat[]? WorkFormats,
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<PaginatedList<VacancyDto>>;

    public class GetVacanciesFilteredQueryHandler : IRequestHandler<GetVacanciesFilteredQuery, PaginatedList<VacancyDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetVacanciesFilteredQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<VacancyDto>> Handle(GetVacanciesFilteredQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Vacancy> query = _context.Vacancies.AsNoTracking();

            if (request.ExpLevels is { Length: > 0 })
            {
                query = query.Where(v => request.ExpLevels.Contains(v.Experience));
            }

            if (request.PrimaryKeys is { Length: > 0 })
            {
                query = query.Where(v => request.PrimaryKeys.Any(c => v.Name.Contains(c)));
            }

            if (request.WorkFormats is { Length: > 0 })
            {
                query = query.Where(v => request.WorkFormats.Contains(v.WorkFormat));
            }

            query = query.OrderByDescending(v => v.CreatedAt);

            var dto = query.ProjectTo<VacancyDto>(_mapper.ConfigurationProvider);

            return await PaginatedList<VacancyDto>.CreateAsync(dto, request.PageNumber, request.PageSize);
        }
    }
}
