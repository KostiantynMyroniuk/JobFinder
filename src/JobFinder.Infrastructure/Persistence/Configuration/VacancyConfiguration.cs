using JobFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Persistence.Configuration
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.Property(j => j.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(j => j.WorkFormat)
                .HasConversion<string>();

            builder.Property(j => j.EnglishLevel)
                .HasConversion<string>();
                
        }
    }
}
