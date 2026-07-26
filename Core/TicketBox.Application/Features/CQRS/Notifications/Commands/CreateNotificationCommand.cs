using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.Notifications.Commands
{
    public class CreateNotificationCommand : IRequest<bool>
    {
        public string AppUserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
