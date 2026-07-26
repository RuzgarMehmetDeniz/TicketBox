using MediatR;

namespace TicketBox.Application.Features.CQRS.Payments.Commands
{
    public class UpdatePaymentCommand : IRequest<bool>
    {
        public int PaymentId { get; set; }
        public string Status { get; set; }
        public string? TransactionReference { get; set; }
    }
}