using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Enums;
using InterviewService.Domain.Resourses;
using InterviewService.InterviewAPI.Services.CurrentUserService;
using InterviewService.InterviewAPI.Services.InterviewService;
using Microsoft.EntityFrameworkCore;

namespace InterviewService.InterviewAPI.Managers.InterviewManager
{
    public class InterviewManager : IInterviewManager
    {
        private readonly IMapper _mapper;
        private readonly IInterviewService _interviewService;
        private readonly ICurrentUserService _currentUserService;

        public InterviewManager(IMapper mapper, IInterviewService interviewService, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _interviewService = interviewService;
            _currentUserService = currentUserService;
        }

        public async Task<InterviewResponse> AddInterview(InterviewRequest request, CancellationToken cancellationToken)
        {
            var interview = new Interview();
            _mapper.Map(request, interview);
            interview.CreatedUserId = _currentUserService.UserId;
            await _interviewService.CreateInterview(interview, cancellationToken);
            return _mapper.Map<InterviewResponse>(interview);
        }

        public async Task UpdateInterview(int id, InterviewRequest request, CancellationToken cancellationToken)
        {
            var interview = await _interviewService.GetInterview(id, cancellationToken: cancellationToken);
            _mapper.Map(request, interview);
            await _interviewService.UpdateInterview(id, interview, cancellationToken);
        }

        public Task<List<InterviewResponse>> GetInterviews(int pageIndex, int pageSize, InterviewType type, CancellationToken cancellationToken)
        {
            var interviews = _interviewService.GetInterviews();
            if (type == InterviewType.ForUser)
            {
                interviews = interviews.Where(interview => interview.CreatedUserId == _currentUserService.UserId);
            }

            if (type == InterviewType.ForAnswer)
            {
                interviews = interviews.Where(interview => interview.Answers.
                    All(answer => answer.UserId != _currentUserService.UserId) && interview.IsPublished && !interview.IsDeleted && interview.InterviewStart < DateTime.Now &&
                    interview.InterviewEnd > DateTime.Now);

            }
            
            interviews = interviews.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            
            return _mapper.ProjectTo<InterviewResponse>(interviews)
                .ToListAsync(cancellationToken);
        }


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