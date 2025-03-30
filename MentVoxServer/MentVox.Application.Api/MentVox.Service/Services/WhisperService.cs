using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MentVox.Core.Interfaces;
using MentVox.Core.Models.ConversationModels;

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

        public IEnumerable<Conversation> GetAll()
        {
            return _conversations;
        }

        public Conversation GetById(int id)
        {
            return _conversations.FirstOrDefault(c => c.ConversationId == id);
        }

        public void Create(Conversation convers)
        {
            convers.ConversationId = _conversations.Count + 1; // Assign a new ID
            _conversations.Add(convers);
        }

        public void Update(Conversation convers)
        {
            var existingItem = GetById(convers.ConversationId);
            if (existingItem != null)
            {
                existingItem.UserMessage = convers.UserMessage;
                existingItem.BotResponse = convers.BotResponse;
            }
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
            {
                _conversations.Remove(item);
            }
        }
    }
}

