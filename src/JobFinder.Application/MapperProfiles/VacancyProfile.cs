using AutoMapper;
using JobFinder.Application.Features.Vacancies.Commands.CreateVacancy;
using JobFinder.Application.Features.Vacancies.Queries.GetVacancies;
using JobFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.MapperProfiles
{
    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<Vacancy, VacancyDto>()
                .ForMember(
                    d => d.MinutesAgo,
                    opt => opt.MapFrom(
                        s => (int) EF.Functions.DateDiffMinute(s.CreatedAt, DateTime.UtcNow)
                    )
                );

            CreateMap<VacancyDto, Vacancy>();

            CreateMap<CreateVacancyCommand, Vacancy>();

        }
    }
}
