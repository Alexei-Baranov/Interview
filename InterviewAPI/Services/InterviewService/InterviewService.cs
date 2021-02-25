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
        private readonly ApplicationDbContext _dbContext;

        public InterviewService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateInterview(Interview request, CancellationToken cancellationToken)
        {
            await _dbContext.Interviews.AddAsync(request, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateInterview(int id, Interview request, CancellationToken cancellationToken)
        {
            _dbContext.Update(request);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteInterview(Interview interview, CancellationToken cancellationToken)
        {
            if (!interview.IsDeleted)
            {
                interview.IsDeleted = true;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public IQueryable<Interview> GetInterviews(bool includedDeleted = default)
        {
            IQueryable<Interview> interviews = _dbContext.Interviews;

            if (includedDeleted is false)
            {
                interviews = interviews.Where(interview => !interview.IsDeleted);
            }

            return interviews;
        }

        public async Task<Interview> GetInterview(int id, bool includeDeleted = default, CancellationToken cancellationToken = default)
        {
            var interview = await _dbContext.Interviews.FirstOrDefaultAsync(interview => interview.Id == id, cancellationToken);

            if (interview is null)
            {
                throw new EntityNotFoundException();
            }

            return interview;
        }
    }
}