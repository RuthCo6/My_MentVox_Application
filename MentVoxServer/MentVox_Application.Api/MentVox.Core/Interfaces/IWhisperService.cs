using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Interfaces
{
    public class IWhisperService
    {
        public Task<string> TranscribeAudioAsync(Stream stream, string fileName)
        {
            throw new NotImplementedException();
        }

        Task<string> TranscribeAudioAsync(Stream audioStream,
            string fileName);
    }
}
