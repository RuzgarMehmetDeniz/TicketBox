using MediatR;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Commands
{
    public class UpdateChatMessageCommand : IRequest<bool>
    {
        public int ChatMessageId { get; set; }
        public string Content { get; set; }
    }
}