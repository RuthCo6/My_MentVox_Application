using MentVox.Core.DTOs;
using MentVox.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static MentVox.Service.Services.ChatGptService;

namespace MentVox.Service.Services
{
    internal class ChatGptService : IChatGptService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ChatGptService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAIApiKey"];
        }


        public IEnumerable<Item> GetAllItems() => _httpClient.GetAll();

        public Item GetItemById(int id) => _httpClient.GetById(id);

        public void CreateItem(Item item) => _httpClient.Add(item);

        public void UpdateItem(Item item) => _httpClient.Update(item);

        public void DeleteItem(int id) => _httpClient.Delete(id);
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
