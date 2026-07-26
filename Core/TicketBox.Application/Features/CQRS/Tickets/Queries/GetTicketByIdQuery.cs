using MediatR;
using TicketBox.Application.Features.CQRS.Tickets.Results;

namespace TicketBox.Application.Features.CQRS.Tickets.Queries
{
    public class GetTicketByIdQuery : IRequest<GetTicketByIdQueryResult>
    {
        public int TicketId { get; set; }
    }
}