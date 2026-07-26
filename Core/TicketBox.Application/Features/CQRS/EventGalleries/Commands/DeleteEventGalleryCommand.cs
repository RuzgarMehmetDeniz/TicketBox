using MediatR;

namespace TicketBox.Application.Features.CQRS.EventGalleries.Commands
{
    public class DeleteEventGalleryCommand : IRequest<bool>
    {
        public int EventGalleryId { get; set; }
    }
}