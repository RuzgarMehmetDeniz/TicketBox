using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Tickets.Results;

namespace TicketBox.Application.Features.Mediator.Tickets.Queries
{
    public class GetByIdTicketQuery:IRequest<GetByIdTicketQueryResult>
    {
        public int Id { get; set; }
    }
}
