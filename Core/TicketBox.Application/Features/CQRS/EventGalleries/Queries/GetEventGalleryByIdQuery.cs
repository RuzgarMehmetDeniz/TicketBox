using MediatR;
using TicketBox.Application.Features.CQRS.EventGalleries.Results;

namespace TicketBox.Application.Features.CQRS.EventGalleries.Queries
{
    public class GetEventGalleryByIdQuery : IRequest<GetEventGalleryByIdQueryResult>
    {
        public int EventGalleryId { get; set; }
    }
}