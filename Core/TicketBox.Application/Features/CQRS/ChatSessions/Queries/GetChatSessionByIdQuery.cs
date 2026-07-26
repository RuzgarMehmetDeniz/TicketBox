using MediatR;
using TicketBox.Application.Features.CQRS.ChatSessions.Results;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Queries
{
    public class GetChatSessionByIdQuery : IRequest<GetChatSessionByIdQueryResult>
    {
        public int ChatSessionId { get; set; }
    }
}