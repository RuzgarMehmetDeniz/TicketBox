using MediatR;

namespace TicketBox.Application.Features.CQRS.Tickets.Commands
{
    public class DeleteTicketCommand : IRequest<bool>
    {
        public int TicketId { get; set; }
    }
}