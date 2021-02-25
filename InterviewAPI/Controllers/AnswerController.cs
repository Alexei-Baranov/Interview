using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewService.Domain.Resourses;
using InterviewService.InterviewAPI.Managers;
using InterviewService.InterviewAPI.Managers.AnswerManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterviewService.InterviewAPI.Controllers
{
    [ApiController]
    [Route("interview/answer")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerManager _answerManager;

        public AnswerController(IAnswerManager answerManager)
        {
            _answerManager = answerManager;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAnswer(AnswerRequest request, int interviewId,
            CancellationToken cancellationToken)
        {
            await _answerManager.AddAnswer(request, interviewId, cancellationToken);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(AnswerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAnswers(int interviewId, CancellationToken cancellationToken)
        {
            var answers = await _answerManager.GetAnswers(interviewId, cancellationToken);

            if (!answers.Any())
            {
                return NoContent();
            }

            return Ok(answers);
        }
    }
}