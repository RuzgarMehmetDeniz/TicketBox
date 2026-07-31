using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationPayments
{
    public class PaymentByTicketSpecification : BaseSpecification<Payment>
    {
        public PaymentByTicketSpecification(int ticketId) : base(p => p.TicketId == ticketId)
        {
            AddInclude(p => p.Ticket);
        }
    }
}