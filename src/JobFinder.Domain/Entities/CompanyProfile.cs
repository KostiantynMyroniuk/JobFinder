using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Domain.Entities
{
    public class CompanyProfile
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();

        public string? CompanyName { get; set; }
        public int? CompanySize { get; set; }

        public string? Country { get; set; }
        public string[]? Cities { get; set; }

        public string? Description { get; set; }

        public string? ContactEmail { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<Vacancy>? Vacancies { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
