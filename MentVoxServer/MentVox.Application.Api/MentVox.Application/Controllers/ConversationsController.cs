using MentVox.Core.DTOs;
using MentVox.Core.Interfaces;
using MentVox.Core.Models.ConversationModels;
using MentVox.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentVox_Application_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : Controller
    {
        private readonly IConversationService _conversationService;
        private readonly ApplicationDbContext _context; // הוספתי יוניט אוף וורק לשמירת נתונים

        public ConversationController(IConversationService conversService, ApplicationDbContext context)
        {
            _conversationService = conversService;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _conversationService.GetAllConvers();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetConversById(int id)
        {
            var item = _conversationService.GetConversById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Conversation convers)
        {
            if (convers == null)
            {
                return BadRequest();
            }
            _context.Conversations.Add(convers); // ודאי שמשתמשים ב-DbContext להוספה
            _context.SaveChanges(); // 🔥 שומר את השינויים

            return CreatedAtAction(nameof(GetConversById), new { id = convers.UserId }, convers);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Conversation convers)
        {
            if (convers == null || convers.UserId != id)
            {
                return BadRequest();
            }

            var existingItem = _conversationService.GetConversById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _conversationService.Update(convers);
            _context.SaveChanges(); // 🔥 חובה לשמור אחרי עדכון

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _conversationService.GetConversById(id);
            if (item == null)
            {
                return NotFound();
            }

            _conversationService.Delete(id);
            _context.SaveChanges(); // 🔥 מחיקה צריכה להיות מאושרת במסד נתונים

            return NoContent();
        }
    }
}