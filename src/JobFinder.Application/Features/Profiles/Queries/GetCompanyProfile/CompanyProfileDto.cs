using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Profiles.Queries.GetCompanyProfile
{
    public class CompanyProfileDto
    {
        public string? CompanyName { get; set; }
        public int? CompanySize { get; set; }

        public string? Country { get; set; }
        public string[]? Cities { get; set; }

        public string? Description { get; set; }

        public string? ContactEmail { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
