using MentVox.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static MentVox.Service.Services.ElevenLabsService;

namespace MentVox.Service.Services
{
    internal class ElevenLabsService : IElevenLabsService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ElevenLabsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ElevenLabsApiKey"];
        }

        public async Task<Stream> TextToSpeechAsync(string text)
        {
            var requestBody = new
            {
                text = text,
                voice = "en_us_male"
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            var response = await _httpClient.PostAsync("https://api.elevenlabs.io/v1/text-to-speech", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            else
            {
                // טיפול בשגיאות
                return null;
            }
        }
    }
}
