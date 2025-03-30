using MentVox.Core.DTOs;
using MentVox.Core.Models.ConversationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Interfaces
{
    public interface IConversationService
    {
        IEnumerable<Conversation> GetAllConvers();
        Conversation GetConversById(int id);
        //Task<IEnumerable<Conversation>> GetByUserIdAsync(int userId);
        public void CreateConvers(Conversation convers);
        void Add(Conversation conversation);
        void Update(Conversation conversation);
        void Delete(int id);
    }
}
