﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.DTOs
{
    public class ChatGptRequestDto
    {
        public string UserMessage { get; set; }
        public int UserId { get; set; }
    }
}
