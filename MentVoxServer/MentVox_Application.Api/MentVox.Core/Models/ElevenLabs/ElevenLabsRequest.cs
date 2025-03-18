using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Models.ElevenLabs
{
    public class ElevenLabsRequest
    {
        public string TextToSynthesize { get; set; }
        public string Voice { get; set; }
    }

}
