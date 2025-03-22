using MentVox.Core.Models.ConversationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Interfaces
{
    public interface IWhisperService
    {
        Task<string> TranscribeAudioAsync(Stream audioFile, string fileName);
        IEnumerable<Conversation> GetAllConvers();
        Conversation GetConversById(int id);
        void CreateConvers(Conversation convers);
        void UpdateConvers(Conversation convers);
        void DeleteConvers(int id);
    }
}
