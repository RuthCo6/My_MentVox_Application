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

        // GET: ConversationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ConversationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConversationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConversationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ConversationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConversationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost("transcribe")]
        public async Task<IActionResult> TranscribeAudio(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
                return BadRequest("No file provided.");

            using var stream = audioFile.OpenReadStream();
            var transcription = await _whisperService.TranscribeAudioAsync(stream, audioFile.FileName);

            return Ok(new WhisperResponseDto { Transcription = transcription });
        }
    }
}
