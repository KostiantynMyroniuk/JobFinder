using JobFinder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Profiles.Queries.GetCandidateProfile
{
    public class CandidateProfileDto
    {
        public string? FirstNameAndLastName { get; set; }

        public string? Position { get; set; }
        public int? SalaryExpectation { get; set; }
        public int? Experience { get; set; }

        public WorkFormat[]? WorkFormats { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }

        public string? ExperienceDescription { get; set; }
    }
}
