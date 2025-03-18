using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Models.Whisper
{
    public class WhisperRequest
    {
        public byte[] AudioData { get; set; }
        public string Language { get; set; }
    }
}
