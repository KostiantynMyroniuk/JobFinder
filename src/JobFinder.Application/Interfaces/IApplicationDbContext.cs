using JobFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace JobFinder.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Vacancy> Vacancies { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<CandidateProfile> CandidateProfiles { get; set; }
        DbSet<CompanyProfile> CompanyProfiles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
