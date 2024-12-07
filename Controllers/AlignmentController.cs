using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/alignment")]
    public class AlignmentController : ControllerBase
    {
        [HttpPost("align-steps")]
        public IActionResult AlignStepByStep([FromBody] AlignmentRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Sequence1) || string.IsNullOrEmpty(request.Sequence2))
            {
                return BadRequest("Invalid input data.");
            }

            var aligner = new SequenceAligner();
            var steps = aligner.AlignStepByStep(request.Sequence1, request.Sequence2);
            return Ok(steps);
        }
    }
}
