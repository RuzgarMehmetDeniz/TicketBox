using MediatR;
using TicketBox.Application.Features.CQRS.Events.Results;

namespace TicketBox.Application.Features.CQRS.Events.Queries
{
    public class GetEventByIdQuery : IRequest<GetEventByIdQueryResult>
    {
        public int EventId { get; set; }
    }
}