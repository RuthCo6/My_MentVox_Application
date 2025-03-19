using MentVox.Core.Models.ConversationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Service.Services
{
    public class ConversationService
    {

        private readonly List<Conversation> _conversations = new List<Conversation>();

        public IEnumerable<Conversation> GetAll() => _conversations;

        public Conversation GetById(int id) => _conversations.FirstOrDefault(c => c.ConversationId == id);

        public void Add(Conversation conversation) => _conversations.Add(conversation);

        public void Update(Conversation conversation)
        {
            var existingConversation = GetById(conversation.ConversationId);
            if (existingConversation != null)
            {
                existingConversation.BotResponse = conversation.BotResponse;
                existingConversation.UserMessage = conversation.UserMessage;
            }
        }

        public void Delete(int id)
        {
            var conversation = GetById(id);
            if (conversation != null)
            {
                _conversations.Remove(conversation);
            }
        }
    }
}
