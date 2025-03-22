using MentVox.Core.Interfaces;
using MentVox.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentVox.Core;
using MentVox.Data;
//using Fluent.Infrastructure.FluentModel;
//using System.Data.Entity.Infrastructure;

namespace MentVox_Application_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhisperController : ControllerBase
    {
        private readonly IWhisperService _whisperService;

        public WhisperController(IWhisperService whisperService, ApplicationDbContext context)
        {
            _whisperService = whisperService;
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        // POST: api/whisper/transcribe
        [HttpPost("transcribe")]
        public async Task<ActionResult<string>> TranscribeAudio([FromForm] Stream audioFile, string fileName)
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                return BadRequest("Invalid audio file.");
            }

            var transcription = await _whisperService.TranscribeAudioAsync(audioFile, fileName);
            return Ok(transcription);
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
