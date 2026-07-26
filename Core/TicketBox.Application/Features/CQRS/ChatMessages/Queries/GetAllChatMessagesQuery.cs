using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.ChatMessages.Results;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Queries
{
    public class GetAllChatMessagesQuery : IRequest<List<GetChatMessageQueryResult>>
    {
    }
}