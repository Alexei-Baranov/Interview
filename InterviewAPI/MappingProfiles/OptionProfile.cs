using AutoMapper;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Resourses;

namespace InterviewService.InterviewAPI.MappingProfiles
{
    public class OptionProfile : Profile
    {
        public OptionProfile()
        {
            CreateMap<Option, OptionResponse>(MemberList.Destination);
            CreateMap<OptionRequest, Option>(MemberList.Source);
            CreateMap<Option, OptionRequest>(MemberList.Destination);
        }
    }
}