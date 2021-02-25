using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Resourses;

namespace InterviewService.InterviewAPI.Services.OptionService
{
    public interface IOptionService
    {
        void ValidateAndThrow(OptionRequest request, Interview interview, CancellationToken cancellationToken);
        IQueryable<Option> GetInterviewOptions(int interviewId);
        
        Task <Option> GetOption(int optionId, CancellationToken cancellationToken);
    }
}