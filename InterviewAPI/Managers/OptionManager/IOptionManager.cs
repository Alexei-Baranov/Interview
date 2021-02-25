using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterviewService.Domain.Resourses;

namespace InterviewService.InterviewAPI.Managers.OptionManager
{
    public interface IOptionManager
    {
        Task<OptionResponse> AddOption(OptionRequest request, int interviewId, CancellationToken cancellationToken);
        
        Task DeleteOption(OptionRequest request, int interviewId, CancellationToken cancellationToken);

        Task<List<OptionResponse>> GetOptions(int interviewId, CancellationToken cancellationToken);
    }
}