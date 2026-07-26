using MediatR;

namespace TicketBox.Application.Features.CQRS.Refunds.Commands
{
    public class DeleteRefundCommand : IRequest<bool>
    {
        public int RefundId { get; set; }
    }
}