using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Services
{
    public class ToolDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public object ParametersSchema { get; set; }
        public Func<JsonElement, Task<string>> Execute { get; set; }
    }

    public interface IOpenAiChatService
    {
        Task<string> GetReplyAsync(List<(string Role, string Content)> conversation, CancellationToken cancellationToken);
        Task<string> TranscribeAsync(Stream audioStream, string fileName, CancellationToken cancellationToken);
        Task<byte[]> GetSpeechAsync(string text, CancellationToken cancellationToken);
        Task<string> GetMoodBasedReplyAsync(List<(string Role, string Content)> conversation, CancellationToken cancellationToken);
        Task<string> AskWithToolsAsync(string systemPrompt, string userQuestion, List<ToolDefinition> tools, CancellationToken cancellationToken);
    }
}