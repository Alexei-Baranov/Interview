using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterviewService.Domain.Resourses;

namespace InterviewService.InterviewAPI.Managers.InterviewManager
{
    public interface IInterviewManager
    {
        public Task<InterviewResponse> AddInterview(InterviewRequest request, CancellationToken cancellationToken);
        Task UpdateInterview(int id, InterviewRequest request, CancellationToken cancellationToken);
        Task<List<InterviewResponse>> GetInterviews(CancellationToken cancellationToken);
        Task<InterviewResponse> GetInterview(int id, CancellationToken cancellationToken);
        Task DeleteInterview(int id, CancellationToken cancellationToken);
    }
}