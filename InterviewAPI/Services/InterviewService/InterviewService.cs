using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewService.Application.Common.Exceptions;
using InterviewService.Domain.Entities;
using InterviewService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InterviewService.InterviewAPI.Services.InterviewService
{
    public class InterviewService : IInterviewService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public InterviewService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task CreateInterview(Interview request, CancellationToken cancellationToken)
        {
            await _applicationDbContext.Interviews.AddAsync(request, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateInterview(int id, Interview request, CancellationToken cancellationToken)
        {
            _applicationDbContext.Update(request);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteInterview(Interview interview, CancellationToken cancellationToken)
        {
            if (!interview.IsDeleted)
            {
                interview.IsDeleted = true;
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public IQueryable<Interview> GetInterviews(bool includedDeleted = default)
        {
            IQueryable<Interview> interviews = _applicationDbContext.Interviews;

            if (includedDeleted is false)
            {
                interviews = interviews.Where(interview => !interview.IsDeleted).OrderBy(interview => interview.Id);
            }

            return interviews;
        }

        public async Task<Interview> GetInterview(int id, bool includeDeleted = default, CancellationToken cancellationToken = default)
        {
            var interview = await _applicationDbContext.Interviews.Include(interview1 => interview1.Answers).FirstOrDefaultAsync(interview => interview.Id == id, cancellationToken);

            if (interview is null)
            {
                throw new EntityNotFoundException();
            }

            return interview;
        }
    }
}