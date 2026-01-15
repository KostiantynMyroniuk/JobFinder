using AutoMapper;
using JobFinder.Application.Interfaces;
using JobFinder.Domain.Entities;
using JobFinder.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Vacancies.Commands.CreateVacancy
{
    public record CreateVacancyCommand(

        string Name,
        string Description,
        WorkFormat WorkFormat,
        string Location,
        int Experience,
        EnglishLevel EnglishLevel

    ) : IRequest<int>;
    

    public class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateVacancyCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Vacancy>(request);

            _context.Vacancies.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            
            return entity.Id;
        }
    }
}
