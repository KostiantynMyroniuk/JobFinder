using JobFinder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserType UserType { get; set; } = UserType.None;

        public CandidateProfile? CandidateProfile { get; set; }
        public CompanyProfile? CompanyProfile { get; set; }

        public void InitializeProfile()
        {
            if (UserType == UserType.Employer)
                CompanyProfile = new CompanyProfile() { UserId = this.Id };
            else if (UserType == UserType.Candidate)
                CandidateProfile = new CandidateProfile() { UserId = this.Id };
        }
        
    }
}
