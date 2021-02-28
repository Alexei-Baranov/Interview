using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Enums;
using InterviewService.Domain.Resourses;
using InterviewService.Infrastructure.Persistence;
using InterviewService.InterviewAPI.Services.CurrentUserService;
using Microsoft.EntityFrameworkCore;

namespace InterviewService.InterviewAPI.Services.OptionService
{
    public class OptionService : IOptionService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _userService;

        public OptionService(ApplicationDbContext applicationDbContext, ICurrentUserService userService)
        {
            _applicationDbContext = applicationDbContext;
            _userService = userService;
        }

        public void ValidateAndThrow(OptionRequest request, Interview interview, CancellationToken cancellationToken)
        {

            if (interview.IsDeleted)
            {
                throw new ValidationException("Данный опрос был удален");
            }

            if (interview.IsPublished)
            {
                throw new ValidationException("Изменять опции опроса можно только до публикации");
            }
            
            if (interview.CreatedUserId != _userService.UserId)
            {
                throw new ValidationException("Вы не являетесь автором");
            }

            if (request.Title.IsNullOrEmpty())
            {
                throw new ValidationException("Вариант ответа не может быть пустым");
            }
        }

        public IQueryable<Option> GetInterviewOptions(int interviewId)
        {
            return _applicationDbContext.Options.Where(option => option.InterviewId == interviewId).OrderBy(option => option.Id);
        }

        public Task<Option> GetOption(int optionId, CancellationToken cancellationToken)
        {
            return _applicationDbContext.Options.FirstOrDefaultAsync(option => option.Id == optionId, cancellationToken);
        }
    }
}