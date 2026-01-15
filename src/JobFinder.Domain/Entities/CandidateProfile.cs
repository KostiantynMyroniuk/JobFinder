using JobFinder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Domain.Entities
{
    public class CandidateProfile
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();

        public string? FirstNameAndLastName { get; set; }

        public string? Position { get; set; }
        public int? SalaryExpectation { get; set; }
        public int? Experience { get; set; }

        public WorkFormat[]? WorkFormats { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }

        public string? ExperienceDescription { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
