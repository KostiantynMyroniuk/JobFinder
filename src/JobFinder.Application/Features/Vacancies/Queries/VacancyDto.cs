using JobFinder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Features.Vacancies.Queries
{
    public class VacancyDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int MinutesAgo { get; set; }
        public WorkFormat WorkFormat { get; set; }
        public string Location { get; set; } = null!;
        public int Experience { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
    }
}
