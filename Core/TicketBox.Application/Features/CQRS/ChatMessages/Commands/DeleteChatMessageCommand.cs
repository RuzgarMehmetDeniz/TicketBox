using MediatR;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Commands
{
    public class DeleteChatMessageCommand : IRequest<bool>
    {
        public int ChatMessageId { get; set; }
    }
}