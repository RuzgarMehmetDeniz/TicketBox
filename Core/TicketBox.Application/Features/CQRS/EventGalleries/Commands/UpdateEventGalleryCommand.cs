using MediatR;

namespace TicketBox.Application.Features.CQRS.EventGalleries.Commands
{
    public class UpdateEventGalleryCommand : IRequest<bool>
    {
        public int EventGalleryId { get; set; }
        public string ImageUrl { get; set; }
    }
}