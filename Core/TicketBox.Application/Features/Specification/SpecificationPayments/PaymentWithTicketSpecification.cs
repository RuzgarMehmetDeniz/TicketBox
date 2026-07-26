using TicketBox.Application.Features.Specification;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationPayments
{
    public class PaymentWithTicketSpecification : BaseSpecification<Payment>
    {
        public PaymentWithTicketSpecification()
        {
            AddInclude(p => p.Ticket);
        }

        public PaymentWithTicketSpecification(int paymentId) : base()
        {
            Criteria = p => p.PaymentId == paymentId;
            AddInclude(p => p.Ticket);
        }
    }
}