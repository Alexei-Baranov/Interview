using System.Linq;
using System.Threading;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Resourses;

namespace InterviewService.InterviewAPI.Services.AnswerService
{
    public interface IAnswerService
    {
        void ValidateAndThrow(AnswerRequest request, Interview interview, CancellationToken cancellationToken);
        IQueryable<Answer> GetInterviewAnswers(int interviewId);
    }
}