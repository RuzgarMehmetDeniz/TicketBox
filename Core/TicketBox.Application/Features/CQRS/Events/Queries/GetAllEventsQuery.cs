using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.Events.Results;

namespace TicketBox.Application.Features.CQRS.Events.Queries
{
    public class GetAllEventsQuery : IRequest<List<GetEventQueryResult>>
    {
    }
}