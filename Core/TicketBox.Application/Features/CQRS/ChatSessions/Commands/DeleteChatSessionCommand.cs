using MediatR;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Commands
{
    public class DeleteChatSessionCommand : IRequest<bool>
    {
        public int ChatSessionId { get; set; }
    }
}