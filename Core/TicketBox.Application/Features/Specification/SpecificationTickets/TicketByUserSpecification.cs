using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationTickets
{
    public class TicketByUserSpecification : BaseSpecification<Ticket>
    {
        public TicketByUserSpecification(string appUserId) : base(t => t.AppUserId == appUserId)
        {
            AddInclude(t => t.Event);
            AddInclude(t => t.AppUser);
            ApplyOrderByDescending(t => t.PurchaseDate);
        }
    }
}