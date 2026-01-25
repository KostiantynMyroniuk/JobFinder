using AutoMapper;
using JobFinder.Application.Features.Profiles.Commands.UpdateCandidateProfile;
using JobFinder.Application.Features.Profiles.Queries.GetCandidateProfile;
using JobFinder.Application.Features.Profiles.Queries.GetCompanyProfile;
using JobFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.MapperProfiles
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            CreateMap<CandidateProfile, CandidateProfileDto>();
            CreateMap<CandidateProfileDto, CandidateProfile>();

            CreateMap<CompanyProfile, CompanyProfileDto>();
            CreateMap<CompanyProfileDto, CompanyProfile>();


            CreateMap<UpdateCandidateProfileCommand, CandidateProfile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

        }
    }
}
