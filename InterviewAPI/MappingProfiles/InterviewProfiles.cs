using AutoMapper;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Resourses;

namespace InterviewService.InterviewAPI.MappingProfiles
{
    public class InterviewProfiles : Profile
    {
        public InterviewProfiles()
        {
            CreateMap<Interview, InterviewResponse>(MemberList.Destination);
            CreateMap<InterviewRequest, Interview>(MemberList.Source);
            CreateMap<Interview, InterviewRequest>(MemberList.Destination);
        }
    }
}