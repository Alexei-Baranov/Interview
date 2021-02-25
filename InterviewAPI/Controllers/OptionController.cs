using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewService.Domain.Resourses;
using InterviewService.InterviewAPI.Managers;
using InterviewService.InterviewAPI.Managers.OptionManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterviewService.InterviewAPI.Controllers
{
    [ApiController]
    [Route("interview/option")]
    public class OptionController : ControllerBase
    {
        private readonly IOptionManager _optionManager;

        public OptionController(IOptionManager optionManager)
        {
            _optionManager = optionManager;
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(OptionResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddOption(OptionRequest request, int interviewId,
            CancellationToken cancellationToken)
        {
            var option = await _optionManager.AddOption(request, interviewId, cancellationToken);
            return Ok(option);
        }

        [HttpGet]
        [ProducesResponseType(typeof(OptionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOptions(int interviewId, CancellationToken cancellationToken)
        {
            var options = await _optionManager.GetOptions(interviewId, cancellationToken);

            if (!options.Any())
            {
                return NoContent();
            }

            return Ok(options);
        }
        
        [HttpDelete("{optionId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSchema(OptionRequest request, int interviewId, CancellationToken cancellationToken)
        {
            await _optionManager.DeleteOption(request, interviewId, cancellationToken);
            return NoContent();
        }
        
    }
}