using MediatR;

namespace TicketBox.Application.Features.CQRS.Refunds.Commands
{
    public class UpdateRefundCommand : IRequest<bool>
    {
        public int RefundId { get; set; }
        public string Status { get; set; }
    }
}