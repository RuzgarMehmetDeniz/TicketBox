using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.Services
{
    public interface IOpenAiChatService
    {
        Task<string> GetReplyAsync(List<(string Role, string Content)> conversation, CancellationToken cancellationToken);
        Task<string> TranscribeAsync(Stream audioStream, string fileName, CancellationToken cancellationToken);
        Task<byte[]> GetSpeechAsync(string text, CancellationToken cancellationToken);
        Task<string> GetMoodBasedReplyAsync(List<(string Role, string Content)> conversation, CancellationToken cancellationToken);
    }
}