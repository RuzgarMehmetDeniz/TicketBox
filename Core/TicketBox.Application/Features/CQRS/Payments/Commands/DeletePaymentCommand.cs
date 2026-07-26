using MediatR;

namespace TicketBox.Application.Features.CQRS.Payments.Commands
{
    public class DeletePaymentCommand : IRequest<bool>
    {
        public int PaymentId { get; set; }
    }
}