using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.Tickets.Results;

namespace TicketBox.Application.Features.CQRS.Tickets.Queries
{
    public class GetMyTicketsQuery : IRequest<List<GetTicketQueryResult>>
    {
        public string AppUserId { get; set; }
    }
}