using TicketBox.Application.Features.Specification;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationEventGalleries
{
    public class EventGalleryWithEventSpecification : BaseSpecification<EventGallery>
    {
        public EventGalleryWithEventSpecification()
        {
            AddInclude(g => g.Event);
        }

        public EventGalleryWithEventSpecification(int eventGalleryId) : base()
        {
            Criteria = g => g.EventGalleryId == eventGalleryId;
            AddInclude(g => g.Event);
        }
    }
}