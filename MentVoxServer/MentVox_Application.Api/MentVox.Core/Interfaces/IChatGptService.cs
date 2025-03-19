using MentVox.Core.DTOs;
using MentVox.Core.Models.ConversationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Interfaces
{
    public interface IChatGptService
    {
        Task<ChatGptResponseDto> GetChatResponseAsync(string inputText);
        //void CreateUser(UserDto userDto);
        IEnumerable<Conversation> GetAllConvers();
        Conversation GetConversById(int id);
        void CreateConvers(Conversation item);
        void Update(Conversation item);
        void Delete(int id);
    }
}
