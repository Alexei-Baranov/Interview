using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterviewService.Domain.Resourses;

namespace InterviewService.InterviewAPI.Managers.AnswerManager
{
    public interface IAnswerManager
    {
        Task AddAnswer(AnswerRequest request, int interviewId, CancellationToken cancellationToken);

        Task<List<AnswerResponse>> GetAnswers(int interviewId, CancellationToken cancellationToken);
    }
}