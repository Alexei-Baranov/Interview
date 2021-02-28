using System.Threading;
using System.Threading.Tasks;
using InterviewService.Application.Common.Security;
using InterviewService.Domain.Enums;
using InterviewService.Domain.Resourses;
using InterviewService.InterviewAPI.Managers;
using InterviewService.InterviewAPI.Managers.InterviewManager;
using Microsoft.AspNetCore.Mvc;

namespace InterviewService.InterviewAPI.Controllers
{   
    [Authorize]
    [Route("interview")]
    [ApiController]
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewManager _interviewManager;

        public InterviewsController(IInterviewManager interviewManager)
        {
            _interviewManager = interviewManager;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInterview( int id, CancellationToken cancellationToken, bool includeDeleted = default)
        {
            var interview = await _interviewManager.GetInterview(id, cancellationToken);
            return Ok(interview);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetInterviews( InterviewType type, CancellationToken cancellationToken, int pageIndex = 1, int pageSize = 1)
        {
            var interview = await _interviewManager.GetInterviews(pageIndex, pageSize, type, cancellationToken);
            return Ok(interview);
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateInterview(InterviewRequest request, CancellationToken cancellationToken)
        {
            var interviewOutputDto = await _interviewManager.AddInterview(request, cancellationToken);
            return Ok(interviewOutputDto) ;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateInterview(int id, InterviewRequest request, CancellationToken cancellationToken)
        {
            await _interviewManager.UpdateInterview(id, request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteInterview(int id, CancellationToken cancellationToken)
        {
            await _interviewManager.DeleteInterview(id, cancellationToken);
            return NoContent();
        }
    }
}