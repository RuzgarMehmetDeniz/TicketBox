using MediatR;
using TicketBox.Application.Features.CQRS.Refunds.Results;

namespace TicketBox.Application.Features.CQRS.Refunds.Queries
{
    public class GetRefundByIdQuery : IRequest<GetRefundByIdQueryResult>
    {
        public int RefundId { get; set; }
    }
}