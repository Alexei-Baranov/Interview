using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewService.Domain.Entities;

namespace InterviewService.InterviewAPI.Services.InterviewService
{
    public interface IInterviewService
    {
        Task CreateInterview(Interview request, CancellationToken cancellationToken);

        Task UpdateInterview( int id, Interview request, CancellationToken cancellationToken);

        Task DeleteInterview(Interview interview, CancellationToken cancellationToken);
        IQueryable<Interview> GetInterviews(bool includedDeleted = default);

        Task<Interview> GetInterview(int id, bool includeDeleted = default,
            CancellationToken cancellationToken = default);
    }
}