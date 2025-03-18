using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Models.ElevenLabs
{
    public class ElevenLabsResponse
    {
        public byte[] SynthesizedAudio { get; set; }
        public string AudioFormat { get; set; }
    }
}
