using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Models.Whisper
{
    public class WhisperResponse
    {
        public string TranscribedText { get; set; }
        public double Confidence { get; set; }
    }
}
