using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.EventGalleries.Results;

namespace TicketBox.Application.Features.CQRS.EventGalleries.Queries
{
    public class GetAllEventGalleriesQuery : IRequest<List<GetEventGalleryQueryResult>>
    {
    }
}