using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.ChatSessions.Results;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Queries
{
    public class GetAllChatSessionsQuery : IRequest<List<GetChatSessionQueryResult>>
    {
    }
}