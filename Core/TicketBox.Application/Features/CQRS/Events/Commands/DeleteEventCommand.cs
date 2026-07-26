using MediatR;

namespace TicketBox.Application.Features.CQRS.Events.Commands
{
    public class DeleteEventCommand : IRequest<bool>
    {
        public int EventId { get; set; }
    }
}