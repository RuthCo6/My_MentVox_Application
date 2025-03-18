using MentVox.Core.DTOs;
using MentVox.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MentVox_Application_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : Controller
    {
        private readonly IWhisperService _whisperService;

        public ConversationController(IWhisperService whisperService)
        {
            _whisperService = whisperService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _whisperService.GetAllConvers();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _whisperService.GetConversById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ConversationController convers)
        {
            if (convers == null)
            {
                return BadRequest();
            }
            _whisperService.CreateConvers(convers);
            return CreatedAtAction(nameof(GetById), new { id = convers.Id }, convers);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ConversationController convers)
        {
            if (convers == null || convers.Id != id)
            {
                return BadRequest();
            }

            var existingItem = _whisperService.GetConversById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _whisperService.UpdateConvers(convers);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _whisperService.GetConversById(id);
            if (item == null)
            {
                return NotFound();
            }

            _whisperService.DeleteConvers(id);
            return NoContent();
        }
    }

    //[HttpPost("transcribe")]
    //public async Task<IActionResult> TranscribeAudio(IFormFile audioFile)
    //{
    //    if (audioFile == null || audioFile.Length == 0)
    //        return BadRequest("No file provided.");

    //    using var stream = audioFile.OpenReadStream();
    //    var transcription = await _whisperService.TranscribeAudioAsync(stream, audioFile.FileName);

    //    return Ok(new WhisperResponseDto { Transcription = transcription });
    //}
}
