﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Models
{
    public class ChatGpt
    {
        public string UserMessage { get; set; }
        public int UserId { get; set; }
        public string BotResponse { get; set; }
        public DateTime ResponseTime { get; set; }
    }

}
