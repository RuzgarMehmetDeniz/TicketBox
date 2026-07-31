using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationRefunds
{
    public class RefundByTicketSpecification : BaseSpecification<Refund>
    {
        public RefundByTicketSpecification(int ticketId) : base(r => r.TicketId == ticketId)
        {
            AddInclude(r => r.Ticket); // GetRefundByIdQueryResult mapping'i src.Ticket.PNRCode kullanıyor
        }
    }
}