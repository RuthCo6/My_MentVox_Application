using MentVox.Core.DTOs;
using MentVox.Core.Interfaces;
using MentVox.Core.Models.ConversationModels;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MentVox.Service.Services
{
    public class ChatGptService : IChatGptService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ConversationService _conversationService;

        public ChatGptService(HttpClient httpClient,IConfiguration configuration, ConversationService conversationService)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAIApiKey"];
            _conversationService = conversationService;
        }

        public IEnumerable<Conversation> GetAllConvers() => _conversationService.GetAll();

        public Conversation GetConversById(int id) => _conversationService.GetById(id);

        public void CreateConvers(Conversation convers) => _conversationService.Add(convers);

        public void Update(Conversation convers) => _conversationService.Update(convers);

        public void Delete(int id) => _conversationService.Delete(id);

        public async Task<ChatGptResponseDto> GetChatResponseAsync(string inputText)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = inputText }
            }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ChatGptResponseDto>(jsonResponse);
            }
            else
            {
                // טיפול בשגיאות
                return null;
            }

        }

    }
}
