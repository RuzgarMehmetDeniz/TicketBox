using TicketBox.Application.Features.Specification;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationEvents
{
    public class EventWithCategorySpecification : BaseSpecification<Event>
    {
        public EventWithCategorySpecification()
        {
            AddInclude(e => e.Category);
        }

        public EventWithCategorySpecification(int eventId) : base()
        {
            Criteria = e => e.EventId == eventId;
            AddInclude(e => e.Category);
        }
    }
}