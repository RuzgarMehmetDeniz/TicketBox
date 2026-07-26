using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.AuditLogs.Commands
{
    public class DeleteAuditLogCommand : IRequest<bool>
    {
        public int AuditLogId { get; set; }
    }
}
