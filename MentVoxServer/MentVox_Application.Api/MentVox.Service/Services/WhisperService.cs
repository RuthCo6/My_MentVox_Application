using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MentVox.Core.Interfaces;
using MentVox.Core.Models.ConversationModels;
using Microsoft.AspNetCore.Http;

namespace MentVox.Service.Services
{
    public class WhisperService : IWhisperService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "YOUR_OPENAI_API_KEY"; // אפשר להוציא ל-Settings אח"כ

        public WhisperService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }
        public async Task<string> TranscribeAudioAsync(Stream audioFile, string fileName)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(audioFile), "file", fileName);
            content.Add(new StringContent("whisper-1"), "model");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/audio/transcriptions", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Whisper API failed: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();
            var transcription = System.Text.Json.JsonDocument.Parse(json)
                .RootElement.GetProperty("text").GetString();

            return transcription ?? string.Empty;
        }
        private readonly List<Conversation> _conversations = new List<Conversation>();

        public IEnumerable<Conversation> GetAllConvers()
        {
            return _conversations;
        }

        public Conversation GetConversById(int id)
        {
            return _conversations.FirstOrDefault(c => c.ConversationId == id);
        }

        public void CreateConvers(Conversation convers)
        {
            convers.ConversationId = _conversations.Count + 1; // Assign a new ID
            convers.CreatedAt = DateTime.UtcNow; // Set the creation time
            _conversations.Add(convers);
        }

        public void UpdateConvers(Conversation convers)
        {
            var existingItem = GetConversById(convers.ConversationId);
            if (existingItem != null)
            {
                existingItem.UserMessage = convers.UserMessage;
                existingItem.BotResponse = convers.BotResponse;
                existingItem.CreatedAt = convers.CreatedAt; // Update creation time if needed
            }
        }

        public void DeleteConvers(int id)
        {
            var item = GetConversById(id);
            if (item != null)
            {
                _conversations.Remove(item);
            }
        }
    }
}

