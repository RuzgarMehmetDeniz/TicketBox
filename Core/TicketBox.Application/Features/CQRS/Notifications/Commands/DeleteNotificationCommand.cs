using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.Notifications.Commands
{
    public class DeleteNotificationCommand : IRequest<bool>
    {
        public int NotificationId { get; set; }
    }
}
