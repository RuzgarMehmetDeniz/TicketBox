using MediatR;

namespace TicketBox.Application.Features.CQRS.Tickets.Commands
{
    public class UpdateTicketCommand : IRequest<bool>
    {
        public int TicketId { get; set; }
        public string? TicketImageUrl { get; set; }
        public string Status { get; set; }
        public bool IsEmailSent { get; set; }
    }
}