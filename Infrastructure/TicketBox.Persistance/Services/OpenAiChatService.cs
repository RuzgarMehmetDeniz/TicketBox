using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TicketBox.Application.Features.Services;

namespace TicketBox.Persistance.Services
{
    public class OpenAiChatService : IOpenAiChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;

        public OpenAiChatService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAI:ApiKey"];
            _model = configuration["OpenAI:Model"] ?? "gpt-4o-mini";
        }

        public async Task<string> GetReplyAsync(List<(string Role, string Content)> conversation, CancellationToken cancellationToken)
        {
            var messages = conversation.Select(m => new { role = m.Role, content = m.Content }).ToList();

            var requestBody = new
            {
                model = _model,
                messages,
                temperature = 0.6,
                max_tokens = 500
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"OpenAI API hatası ({response.StatusCode}): {responseBody}");

            using var doc = JsonDocument.Parse(responseBody);
            var content = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return content?.Trim() ?? "Üzgünüm, şu anda cevap veremiyorum.";
        }
        public async Task<string> TranscribeAsync(Stream audioStream, string fileName, CancellationToken cancellationToken)
        {
            using var content = new MultipartFormDataContent();
            using var streamContent = new StreamContent(audioStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("audio/webm");

            content.Add(streamContent, "file", fileName);
            content.Add(new StringContent("whisper-1"), "model");
            content.Add(new StringContent("tr"), "language"); // Türkçe olarak zorluyoruz

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/audio/transcriptions")
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Whisper API hatası ({response.StatusCode}): {responseBody}");

            using var doc = JsonDocument.Parse(responseBody);
            return doc.RootElement.GetProperty("text").GetString() ?? "";
        }

        public async Task<byte[]> GetSpeechAsync(string text, CancellationToken cancellationToken)
        {
            var requestBody = new
            {
                model = "tts-1",
                input = text,
                voice = "nova"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/audio/speech")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new Exception($"TTS API hatası ({response.StatusCode}): {errorBody}");
            }

            return await response.Content.ReadAsByteArrayAsync(cancellationToken);
        }
    }
}