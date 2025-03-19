using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Models.ConversationModels
{
    public class Conversation
    {
        public string UserMessage { get; set; }
        public int UserId { get; set; }
        public int ConversationId { get; set; }
        public string BotResponse { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
