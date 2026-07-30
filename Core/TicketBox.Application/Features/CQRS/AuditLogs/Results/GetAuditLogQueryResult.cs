using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.AuditLogs.Results
{
    public class GetAuditLogQueryResult
    {
        public int AuditLogId { get; set; }
        public string AppUserId { get; set; }
        public string AppUserName { get; set; }
        public string Action { get; set; }
        public string? Details { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
