using MediatR;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Commands
{
    public class CreateChatMessageCommand : IRequest<bool>
    {
        public int ChatSessionId { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
    }
}