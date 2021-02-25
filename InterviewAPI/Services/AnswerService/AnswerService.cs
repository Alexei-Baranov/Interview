using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using InterviewService.Domain.Entities;
using InterviewService.Domain.Enums;
using InterviewService.Domain.Resourses;
using InterviewService.Infrastructure.Persistence;
using InterviewService.InterviewAPI.Services.CurrentUserService;

namespace InterviewService.InterviewAPI.Services.AnswerService
{
    public class AnswerService : IAnswerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrentUserService _userService;

        public AnswerService(ApplicationDbContext dbContext, ICurrentUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public void ValidateAndThrow(AnswerRequest request, Interview interview, CancellationToken cancellationToken)
        {
            if (interview.IsDeleted)
            {
                throw new ValidationException("Данный опрос был удален");
            }

            if (interview.Status == InterviewStatus.Draft)
            {
                throw new ValidationException("Не найден опубликованный опрос для ответа");
            }
            
            if (interview.Status == InterviewStatus.Finished)
            {
                throw new ValidationException("Данный опрос закончился");
            }

            /*if (_userService.UserId is null)
            {
                throw new ValidationException("Для прохождения опроса требуется авторизация");
            }*/
            
            if (!request.OptionsId.Any())
            {
                throw new ValidationException("Не выбран вариант ответа");
            }

            if (request.OptionsId.Any(i => _dbContext.Options.FirstOrDefault(option => option.Id == i) == null))
            {
                throw new ValidationException("Некоторые из выбранных ответы не существуют");
            }
            
            if (interview.AllOptionRequired && request.OptionsId.Count != interview.SelectedOptionCount)
            {
                throw new ValidationException("Количество выбраных опций ответа не соответствует настройкам автора");
            }

            if (interview.Answers.FirstOrDefault(answer => answer.UserId == _userService.UserId) != null)
            {
                throw new ValidationException("Вы уже ответили на данный опрос");
            }
        }

        public IQueryable<Answer> GetInterviewAnswers(int interviewId)
        {
            return _dbContext.Answers.Where(answer => answer.InterviewId == interviewId);
        }
    }
}