using MentVox.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VirtualAdvisorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private readonly IChatGptService _chatGptService;

        public ChatGptController(IChatGptService chatGptService)
        {
            _chatGptService = chatGptService;
        }

        // POST: api/chatgpt/message
        [HttpPost("message")]
        public async Task<ActionResult<string>> SendMessage([FromBody] ChatGptRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Message))
            {
                return BadRequest("Invalid request.");
            }

            var response = await _chatGptService.GetResponseAsync(request.Message);
            return Ok(response);
        }
    }
}
