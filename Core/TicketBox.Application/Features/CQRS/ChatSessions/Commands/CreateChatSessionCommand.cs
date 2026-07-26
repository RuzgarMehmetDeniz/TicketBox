using MediatR;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Commands
{
    public class CreateChatSessionCommand : IRequest<int>
    {
        public string AppUserId { get; set; }
    }
}