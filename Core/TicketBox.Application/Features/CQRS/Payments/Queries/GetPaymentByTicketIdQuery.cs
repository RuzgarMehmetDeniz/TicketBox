using MediatR;
using TicketBox.Application.Features.CQRS.Payments.Results;

namespace TicketBox.Application.Features.CQRS.Payments.Queries
{
    public class GetPaymentByTicketIdQuery : IRequest<GetPaymentQueryResult?>
    {
        public int TicketId { get; set; }
    }
}