using MediatR;
using TicketBox.Application.Features.CQRS.Payments.Results;

namespace TicketBox.Application.Features.CQRS.Payments.Queries
{
    public class GetPaymentByIdQuery : IRequest<GetPaymentByIdQueryResult>
    {
        public int PaymentId { get; set; }
    }
}