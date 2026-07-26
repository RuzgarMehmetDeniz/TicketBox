using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AuditLogs.Results;

namespace TicketBox.Application.Features.CQRS.AuditLogs.Queries
{
    public class GetAuditLogByIdQuery : IRequest<GetAuditLogByIdQueryResult>
    {
        public int AuditLogId { get; set; }
    }
}
