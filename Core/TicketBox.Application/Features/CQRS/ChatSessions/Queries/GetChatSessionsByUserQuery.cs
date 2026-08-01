using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.ChatSessions.Results;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Queries
{
    public class GetChatSessionsByUserQuery : IRequest<List<ChatSessionWithMessagesResult>>
    {
        public string AppUserId { get; set; }
    }
}