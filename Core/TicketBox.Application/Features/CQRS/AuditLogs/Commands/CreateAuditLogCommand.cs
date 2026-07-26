using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.AuditLogs.Commands
{
    public class CreateAuditLogCommand : IRequest<bool>
    {
        public string AppUserId { get; set; }
        public string Action { get; set; }
        public string? Details { get; set; }
    }
}
