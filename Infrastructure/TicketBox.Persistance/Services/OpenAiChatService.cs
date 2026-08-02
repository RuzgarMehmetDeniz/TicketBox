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
using TicketBox.Application.Features.Repository;
using TicketBox.Application.Features.Services;
using TicketBox.Domain.Entities;

namespace TicketBox.Persistance.Services
{
    public class OpenAiChatService : IOpenAiChatService
    {
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _apiKey;
        private readonly string _model;

        public OpenAiChatService(HttpClient httpClient, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _httpClient = httpClient;
            _unitOfWork = unitOfWork;
            _apiKey = configuration["OpenAI:ApiKey"];
            _model = configuration["OpenAI:Model"] ?? "gpt-4o-mini";
        }
        public async Task<string> AskWithToolsAsync(string systemPrompt, string userQuestion, List<ToolDefinition> tools, CancellationToken cancellationToken)
        {
            var toolsJson = tools.Select(t => new
            {
                type = "function",
                function = new
                {
                    name = t.Name,
                    description = t.Description,
                    parameters = t.ParametersSchema
                }
            }).ToList();

            var messages = new List<object>
    {
        new { role = "system", content = systemPrompt },
        new { role = "user", content = userQuestion }
    };

            // 1. round: modele soruyu + araç listesini gönderiyoruz
            var firstRequestBody = new
            {
                model = _model,
                messages,
                tools = toolsJson,
                tool_choice = "auto",
                temperature = 0.3
            };

            var firstResponseBody = await PostToOpenAiAsync("https://api.openai.com/v1/chat/completions", firstRequestBody, cancellationToken);

            using var firstDoc = JsonDocument.Parse(firstResponseBody);
            var choiceMessage = firstDoc.RootElement.GetProperty("choices")[0].GetProperty("message");

            if (!choiceMessage.TryGetProperty("tool_calls", out var toolCallsElement) || toolCallsElement.GetArrayLength() == 0)
            {
                // Model araç kullanmadan direkt cevap verdi
                return choiceMessage.GetProperty("content").GetString() ?? "Cevap üretilemedi.";
            }

            // Modelin assistant mesajını (tool_calls dahil) sohbete geri ekliyoruz
            messages.Add(JsonSerializer.Deserialize<object>(choiceMessage.GetRawText()));

            // Her tool_call için ilgili lokal fonksiyonu çalıştırıp sonucu ekliyoruz
            foreach (var toolCall in toolCallsElement.EnumerateArray())
            {
                var toolCallId = toolCall.GetProperty("id").GetString();
                var functionName = toolCall.GetProperty("function").GetProperty("name").GetString();
                var argumentsJson = toolCall.GetProperty("function").GetProperty("arguments").GetString();

                var matchedTool = tools.FirstOrDefault(t => t.Name == functionName);
                string resultJson;

                if (matchedTool == null)
                {
                    resultJson = JsonSerializer.Serialize(new { error = "Bilinmeyen araç." });
                }
                else
                {
                    using var argsDoc = JsonDocument.Parse(string.IsNullOrWhiteSpace(argumentsJson) ? "{}" : argumentsJson);
                    resultJson = await matchedTool.Execute(argsDoc.RootElement);
                }

                messages.Add(new
                {
                    role = "tool",
                    tool_call_id = toolCallId,
                    content = resultJson
                });
            }

            // 2. round: gerçek veriyle birlikte modelden nihai doğal dil cevabını istiyoruz
            var secondRequestBody = new
            {
                model = _model,
                messages,
                temperature = 0.4
            };

            var secondResponseBody = await PostToOpenAiAsync("https://api.openai.com/v1/chat/completions", secondRequestBody, cancellationToken);

            using var secondDoc = JsonDocument.Parse(secondResponseBody);
            return secondDoc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString()
                ?? "Cevap üretilemedi.";
        }

        private async Task<string> PostToOpenAiAsync(string url, object body, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"OpenAI API hatası ({response.StatusCode}): {responseBody}");

            return responseBody;
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

        // ================= MOOD-BASED CHATBOT (function calling) =================

        public async Task<string> GetMoodBasedReplyAsync(List<(string Role, string Content)> conversation, CancellationToken cancellationToken)
        {
            var messages = conversation
                .Select(m => new Dictionary<string, object?> { ["role"] = m.Role, ["content"] = m.Content })
                .ToList();

            var tools = new object[]
            {
                new
                {
                    type = "function",
                    function = new
                    {
                        name = "get_events_by_mood",
                        description = "Kullanıcının o anki ruh haline / moduna uygun, veritabanındaki gerçek ve aktif etkinlikleri getirir.",
                        parameters = new
                        {
                            type = "object",
                            properties = new
                            {
                                mood_description = new
                                {
                                    type = "string",
                                    description = "Kullanıcının ruh halinin serbest metinle kısa özeti (örn. 'yorgun ve sakinlik arıyor', 'enerjik, arkadaşlarıyla kalabalık bir ortam istiyor', 'romantik bir akşam istiyor')."
                                },
                                keywords = new
                                {
                                    type = "array",
                                    items = new { type = "string" },
                                    description = "Bu ruh haline uygun düşebilecek, etkinlik başlığı/açıklaması/kategorisinde aranacak Türkçe anahtar kelimeler (örn. ['tiyatro','sergi'] ya da ['konser','festival'])."
                                }
                            },
                            required = new[] { "mood_description", "keywords" }
                        }
                    }
                }
            };

            using var firstResponse = await SendChatRequestAsync(messages, tools, cancellationToken);

            var choice = firstResponse.RootElement.GetProperty("choices")[0];
            var message = choice.GetProperty("message");

            if (message.TryGetProperty("tool_calls", out var toolCallsElement) && toolCallsElement.GetArrayLength() > 0)
            {
                var toolCall = toolCallsElement[0];
                var toolCallId = toolCall.GetProperty("id").GetString();
                var functionArgsJson = toolCall.GetProperty("function").GetProperty("arguments").GetString();

                var keywords = new List<string>();
                using (var argsDoc = JsonDocument.Parse(string.IsNullOrEmpty(functionArgsJson) ? "{}" : functionArgsJson))
                {
                    if (argsDoc.RootElement.TryGetProperty("keywords", out var kwEl) && kwEl.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var kw in kwEl.EnumerateArray())
                        {
                            var value = kw.GetString();
                            if (!string.IsNullOrWhiteSpace(value))
                                keywords.Add(value);
                        }
                    }
                }

                var matchedEvents = await GetEventsByKeywordsAsync(keywords, cancellationToken);
                var toolResultJson = JsonSerializer.Serialize(matchedEvents);

                // Modelin tool_calls içeren asistan mesajını olduğu gibi geri ekliyoruz
                messages.Add(new Dictionary<string, object?>
                {
                    ["role"] = "assistant",
                    ["content"] = null,
                    ["tool_calls"] = JsonSerializer.Deserialize<object>(toolCallsElement.GetRawText())
                });

                // Tool'un sonucunu ekliyoruz
                messages.Add(new Dictionary<string, object?>
                {
                    ["role"] = "tool",
                    ["tool_call_id"] = toolCallId,
                    ["content"] = toolResultJson
                });

                using var secondResponse = await SendChatRequestAsync(messages, tools: null, cancellationToken);
                var finalContent = secondResponse.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return finalContent?.Trim() ?? "Şu an uygun bir etkinlik önerisi oluşturamadım.";
            }

            return message.TryGetProperty("content", out var contentEl)
                ? (contentEl.GetString()?.Trim() ?? "Üzgünüm, şu anda cevap veremiyorum.")
                : "Üzgünüm, şu anda cevap veremiyorum.";
        }

        private async Task<JsonDocument> SendChatRequestAsync(
            List<Dictionary<string, object?>> messages,
            object[]? tools,
            CancellationToken cancellationToken)
        {
            var requestBodyDict = new Dictionary<string, object?>
            {
                ["model"] = _model,
                ["messages"] = messages,
                ["temperature"] = 0.6,
                ["max_tokens"] = 500
            };

            if (tools != null)
            {
                requestBodyDict["tools"] = tools;
                requestBodyDict["tool_choice"] = "auto";
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestBodyDict), Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"OpenAI API hatası ({response.StatusCode}): {responseBody}");

            return JsonDocument.Parse(responseBody);
        }

        private async Task<List<object>> GetEventsByKeywordsAsync(List<string> keywords, CancellationToken cancellationToken)
        {
            var allEvents = await _unitOfWork.EventRepository.GetAllAsync();

            var candidates = allEvents
                .Where(e => e.IsActive && e.RemainingCapacity > 0 && e.EventDate >= DateTime.Now)
                .ToList();

            List<Event> matched = new();

            if (keywords.Any())
            {
                matched = candidates
                    .Where(e => keywords.Any(k =>
                        (e.Title?.Contains(k, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (e.Description?.Contains(k, StringComparison.OrdinalIgnoreCase) ?? false)))
                    .ToList();
            }

            // Anahtar kelimeyle eşleşme bulunamazsa en yakın tarihli aktif etkinliklere düş
            if (!matched.Any())
                matched = candidates.OrderBy(e => e.EventDate).ToList();

            var topMatches = matched.OrderBy(e => e.EventDate).Take(5).ToList();

            var result = new List<object>();
            foreach (var e in topMatches)
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(e.CategoryId);
                result.Add(new
                {
                    e.Title,
                    Category = category?.CategoryName,
                    Date = e.EventDate.ToString("dd MMM yyyy"),
                    Price = e.Price,
                    RemainingCapacity = e.RemainingCapacity
                });
            }

            return result;
        }
    }
}