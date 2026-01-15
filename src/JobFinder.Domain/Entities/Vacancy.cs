using JobFinder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Domain.Entities
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public WorkFormat WorkFormat { get; set; }
        public string Location { get; set; }
        public int Experience { get; set; }
        public EnglishLevel EnglishLevel { get; set; }

    }
}
