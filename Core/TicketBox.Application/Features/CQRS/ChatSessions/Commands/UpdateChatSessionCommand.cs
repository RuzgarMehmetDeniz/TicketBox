using MediatR;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Commands
{
    public class UpdateChatSessionCommand : IRequest<bool>
    {
        public int ChatSessionId { get; set; }
        public string AppUserId { get; set; }
    }
}