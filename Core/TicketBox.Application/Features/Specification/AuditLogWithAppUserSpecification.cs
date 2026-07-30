using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification
{
    public class AuditLogWithAppUserSpecification : BaseSpecification<AuditLog>
    {
        public AuditLogWithAppUserSpecification() : base()
        {
            AddInclude(x => x.AppUser);
        }

        public AuditLogWithAppUserSpecification(int id) : base(x => x.AuditLogId == id)
        {
            AddInclude(x => x.AppUser);
        }
    }
}