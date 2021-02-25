using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Resourses;
using InterviewService.InterviewAPI.Services.InterviewService;
using InterviewService.InterviewAPI.Services.OptionService;
using Microsoft.EntityFrameworkCore;

namespace InterviewService.InterviewAPI.Managers.OptionManager
{
    public class OptionManager : IOptionManager
    {
        private readonly IInterviewService _interviewService;
        private readonly IMapper _mapper;
        private readonly IOptionService _optionService;

        public OptionManager(IInterviewService interviewService, IMapper mapper, IOptionService optionService)
        {
            _interviewService = interviewService;
            _mapper = mapper;
            _optionService = optionService;
        }

        public async Task<OptionResponse> AddOption(OptionRequest request, int interviewId, CancellationToken cancellationToken)
        {
            var interview = await _interviewService.GetInterview(interviewId, cancellationToken: cancellationToken);

            _optionService.ValidateAndThrow(request, interview, cancellationToken);
            
            var option = Option.Create();

            _mapper.Map(request, option);
            
            interview.AddOption(option);

            await _interviewService.UpdateInterview(interviewId, interview, cancellationToken);

            return _mapper.Map<OptionResponse>(option);
        }

        public async Task DeleteOption(OptionRequest request, int interviewId, CancellationToken cancellationToken)
        {
            var interview = await _interviewService.GetInterview(interviewId, cancellationToken: cancellationToken);

            _optionService.ValidateAndThrow(request, interview, cancellationToken);

            var option = _mapper.Map<Option>(request);

            interview.RemoveOption(option);

            await _interviewService.UpdateInterview(interviewId, interview, cancellationToken);
        }

        public Task<List<OptionResponse>> GetOptions(int interviewId, CancellationToken cancellationToken) =>
            _mapper.ProjectTo<OptionResponse>(_optionService.GetInterviewOptions(interviewId))
                .ToListAsync(cancellationToken);
    }
}