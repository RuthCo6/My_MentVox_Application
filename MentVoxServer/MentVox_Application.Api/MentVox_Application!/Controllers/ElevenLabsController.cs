using MentVox.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VirtualAdvisorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevenLabsController : ControllerBase
    {
        private readonly IElevenLabsService _elevenLabsService;

        public ElevenLabsController(IElevenLabsService elevenLabsService)
        {
            _elevenLabsService = elevenLabsService;
        }

        // POST: api/elevenlabs/synthesize
        [HttpPost("synthesize")]
        public async Task<IActionResult> SynthesizeSpeech([FromBody] SynthesizeRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Text))
            {
                return BadRequest("Invalid request.");
            }

            var audioStream = await _elevenLabsService.SynthesizeSpeechAsync(request.Text);
            return File(audioStream, "audio/mpeg");
        }
    }
}
