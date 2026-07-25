using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.AppUsers.Results;

namespace TicketBox.Application.Features.CQRS.AppUsers.Queries
{
    public class GetAllAppUsersQuery : IRequest<List<GetAppUserQueryResult>>
    {
    }
}
