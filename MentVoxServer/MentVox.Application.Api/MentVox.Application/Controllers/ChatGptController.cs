using MentVox.Core.Interfaces;
using MentVox.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MentVox.Core.Models;

namespace VirtualAdvisorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatGptController : ControllerBase
    {
        private readonly IChatGptService _chatGptService;

        public ChatGptController(IChatGptService chatGptService)
        {
            _chatGptService = chatGptService;
        }

        // POST: api/chatgpt/message
        [HttpPost("message")]
        public async Task<ActionResult<string>> SendMessage([FromBody] ChatGpt request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserMessage))
            {
                return BadRequest("Invalid request.");
            }

            var response = await _chatGptService.GetChatResponseAsync(request.UserMessage);
            return Ok(response);
        }
    }
}