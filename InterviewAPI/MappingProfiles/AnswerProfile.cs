using AutoMapper;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Resourses;

namespace InterviewService.InterviewAPI.MappingProfiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerResponse>(MemberList.Destination);
            CreateMap<AnswerRequest, Answer>(MemberList.Source);
            CreateMap<Answer, AnswerRequest>(MemberList.Destination);
        }
    }
}