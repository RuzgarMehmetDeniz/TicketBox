using TicketBox.Application.Features.Specification;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationTickets
{
    public class TicketWithEventSpecification : BaseSpecification<Ticket>
    {
        public TicketWithEventSpecification()
        {
            AddInclude(t => t.Event);
        }

        public TicketWithEventSpecification(int ticketId) : base()
        {
            Criteria = t => t.TicketId == ticketId;
            AddInclude(t => t.Event);
        }
    }
}