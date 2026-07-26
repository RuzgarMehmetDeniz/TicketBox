using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Notifications.Results;

namespace TicketBox.Application.Features.CQRS.Notifications.Queries
{
    public class GetAllNotificationsQuery : IRequest<List<GetNotificationQueryResult>>
    {
    }
}
