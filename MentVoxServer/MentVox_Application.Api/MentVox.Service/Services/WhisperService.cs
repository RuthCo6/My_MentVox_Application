using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace MentVox.Service.Services
{
    internal class WhisperService : IWhisperService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "YOUR_OPENAI_API_KEY"; // אפשר להוציא ל-Settings אח"כ

        public WhisperService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> TranscribeAudioAsync(Stream audioStream, string fileName)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(audioStream), "file", fileName);
            content.Add(new StringContent("whisper-1"), "model");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/audio/transcriptions", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Whisper API failed: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();
            var transcription = System.Text.Json.JsonDocument.Parse(json)
                .RootElement.GetProperty("text").GetString();

            return transcription ?? string.Empty;
        }
    }

    internal interface IWhisperService
    {
    }
}

