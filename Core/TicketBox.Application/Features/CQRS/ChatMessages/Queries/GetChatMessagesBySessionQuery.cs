using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.ChatMessages.Results;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Queries
{
    public class GetChatMessagesBySessionQuery : IRequest<List<GetChatMessageQueryResult>>
    {
        public int ChatSessionId { get; set; }
    }
}