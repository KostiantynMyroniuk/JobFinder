using JobFinder.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Vacancies.Commands.DeleteVacancy
{
    public record DeleteVacancyCommand(int Id) : IRequest;

    public class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteVacancyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _context.Vacancies.FindAsync(request.Id, cancellationToken);

            if (vacancy == null)
                throw new KeyNotFoundException($"Vacancy not found.");

            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
