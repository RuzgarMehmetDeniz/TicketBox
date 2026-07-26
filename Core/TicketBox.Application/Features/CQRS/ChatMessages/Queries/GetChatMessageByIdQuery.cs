using MediatR;
using TicketBox.Application.Features.CQRS.ChatMessages.Results;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Queries
{
    public class GetChatMessageByIdQuery : IRequest<GetChatMessageByIdQueryResult>
    {
        public int ChatMessageId { get; set; }
    }
}