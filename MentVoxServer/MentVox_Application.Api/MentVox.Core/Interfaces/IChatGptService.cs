using MentVox.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Interfaces
{
    public class IChatGptService
    {
        Task<ChatGptResponseDto> GetChatResponseAsync(string inputText);
    }
}
