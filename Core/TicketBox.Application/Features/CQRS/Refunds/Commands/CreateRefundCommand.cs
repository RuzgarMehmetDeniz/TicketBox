using MediatR;

namespace TicketBox.Application.Features.CQRS.Refunds.Commands
{
    public class CreateRefundCommand : IRequest<int>
    {
        public int TicketId { get; set; }
        public int PaymentId { get; set; }
        public decimal RefundAmount { get; set; }
        public string Reason { get; set; }
    }
}