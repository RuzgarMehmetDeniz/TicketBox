using MediatR;
using TicketBox.Application.Features.CQRS.ChatSessions.Results;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Queries
{
    public class GetLatestChatSessionByUserQuery : IRequest<ChatSessionWithMessagesResult?>
    {
        public string AppUserId { get; set; }
    }
}