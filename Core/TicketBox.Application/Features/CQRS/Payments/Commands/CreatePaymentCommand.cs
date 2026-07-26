using MediatR;

namespace TicketBox.Application.Features.CQRS.Payments.Commands
{
    public class CreatePaymentCommand : IRequest<int>
    {
        public int TicketId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string? TransactionReference { get; set; }
    }
}