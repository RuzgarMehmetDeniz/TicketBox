using MediatR;

namespace TicketBox.Application.Features.CQRS.Tickets.Commands
{
    public class CreateTicketCommand : IRequest<int>
    {
        public int EventId { get; set; }
        public string AppUserId { get; set; }
        public int? CouponId { get; set; }
    }
}