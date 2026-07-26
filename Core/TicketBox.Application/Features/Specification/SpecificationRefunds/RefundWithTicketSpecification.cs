using TicketBox.Application.Features.Specification;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationRefunds
{
    public class RefundWithTicketSpecification : BaseSpecification<Refund>
    {
        public RefundWithTicketSpecification()
        {
            AddInclude(r => r.Ticket);
        }

        public RefundWithTicketSpecification(int refundId) : base()
        {
            Criteria = r => r.RefundId == refundId;
            AddInclude(r => r.Ticket);
        }
    }
}