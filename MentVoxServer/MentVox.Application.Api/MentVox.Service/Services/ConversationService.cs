using MentVox.Core.Interfaces;
using MentVox.Core.Models.ConversationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Service.Services
{
    public class ConversationService : IConversationService
    {

        private readonly List<Conversation> _conversations = new List<Conversation>();

        public IEnumerable<Conversation> GetAllConvers() => _conversations;

        public Conversation GetConversById(int id) => _conversations.FirstOrDefault(c => c.ConversationId == id);

        public void CreateConvers(Conversation convers) => _conversations.Add(convers);
        public void Add(Conversation conversation) => _conversations.Add(conversation);

        public void Update(Conversation conversation)
        {
            var existingConversation = GetConversById(conversation.ConversationId);
            if (existingConversation != null)
            {
                existingConversation.BotResponse = conversation.BotResponse;
                existingConversation.UserMessage = conversation.UserMessage;
            }
        }

        public void Delete(int id)
        {
            var conversation = GetConversById(id);
            if (conversation != null)
            {
                _conversations.Remove(conversation);
            }
        }
    }
}
