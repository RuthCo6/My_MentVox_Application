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
        IEnumerable<Conversation> GetAll();
        Conversation GetById(int id);
        void Create(Conversation convers);
        void Update(Conversation convers);
        void Delete(int id);
    }
}
