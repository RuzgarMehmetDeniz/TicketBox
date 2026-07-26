using MediatR;

namespace TicketBox.Application.Features.CQRS.EventGalleries.Commands
{
    public class CreateEventGalleryCommand : IRequest<int>
    {
        public int EventId { get; set; }
        public string ImageUrl { get; set; }
    }
}