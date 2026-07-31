using MediatR;
using TicketBox.Application.Features.CQRS.Refunds.Results;

namespace TicketBox.Application.Features.CQRS.Refunds.Queries
{
    public class GetRefundByTicketIdQuery : IRequest<GetRefundByIdQueryResult?>
    {
        public int TicketId { get; set; }
    }
}