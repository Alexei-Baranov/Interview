using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Resourses;
using InterviewService.InterviewAPI.Services.InterviewService;
using Microsoft.EntityFrameworkCore;

namespace InterviewService.InterviewAPI.Managers.InterviewManager
{
    public class InterviewManager : IInterviewManager
    {
        private readonly IMapper _mapper;
        private readonly IInterviewService _interviewService;

        public InterviewManager(IMapper mapper, IInterviewService interviewService)
        {
            _mapper = mapper;
            _interviewService = interviewService;
        }

        public async Task<InterviewResponse> AddInterview(InterviewRequest request, CancellationToken cancellationToken)
        {
            var interview = Interview.Create();
            _mapper.Map(request, interview);
            await _interviewService.CreateInterview(interview, cancellationToken);
            return _mapper.Map<InterviewResponse>(interview);
        }

        public async Task UpdateInterview(int id, InterviewRequest request, CancellationToken cancellationToken)
        {
            var interview = _mapper.Map<Interview>(request);
            await _interviewService.UpdateInterview(id, interview, cancellationToken);
        }

        public Task<List<InterviewResponse>> GetInterviews(CancellationToken cancellationToken) => 
            _mapper.ProjectTo<InterviewResponse>(_interviewService.GetInterviews()).ToListAsync(cancellationToken);
        

        
        public async Task<InterviewResponse> GetInterview(int id, CancellationToken cancellationToken)
        {
            var interview = await _interviewService.GetInterview(id, cancellationToken: cancellationToken);
            return _mapper.Map<InterviewResponse>(interview);
        }

        public async Task DeleteInterview(int id, CancellationToken cancellationToken)
        {
            var interview = await _interviewService.GetInterview(id, true, cancellationToken);
            await _interviewService.DeleteInterview(interview, cancellationToken);
        }
    }
}