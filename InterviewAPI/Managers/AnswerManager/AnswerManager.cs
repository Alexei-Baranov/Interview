using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Resourses;
using InterviewService.InterviewAPI.Services.AnswerService;
using InterviewService.InterviewAPI.Services.InterviewService;
using InterviewService.InterviewAPI.Services.OptionService;
using Microsoft.EntityFrameworkCore;

namespace InterviewService.InterviewAPI.Managers.AnswerManager
{
    public class AnswerManager : IAnswerManager
    {
        private readonly IAnswerService _answerService;
        private readonly IMapper _mapper;
        private readonly IInterviewService _interviewService;
        private readonly IOptionService _optionService;

        public AnswerManager(IAnswerService answerService, IMapper mapper, IInterviewService interviewService, IOptionService optionService)
        {
            _answerService = answerService;
            _mapper = mapper;
            _interviewService = interviewService;
            _optionService = optionService;
        }

        public async Task AddAnswer(AnswerRequest request, int interviewId, CancellationToken cancellationToken)
        {
            var interview = await _interviewService.GetInterview(interviewId, cancellationToken: cancellationToken);
            _answerService.ValidateAndThrow(request, interview, cancellationToken);


            foreach (var optionId in request.OptionsId)
            {
                var answer = Answer.Create();
                answer.Option = await _optionService.GetOption(optionId, cancellationToken);
                interview.AddAnswer(answer);
            }
            
            await _interviewService.UpdateInterview(interviewId, interview, cancellationToken);
        }

        public Task<List<AnswerResponse>> GetAnswers(int interviewId, CancellationToken cancellationToken) =>
            _mapper.ProjectTo<AnswerResponse>(_answerService.GetInterviewAnswers(interviewId))
                .ToListAsync(cancellationToken);
    }
}