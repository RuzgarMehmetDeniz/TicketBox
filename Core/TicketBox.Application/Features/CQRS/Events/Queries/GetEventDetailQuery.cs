using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Events.Results;

namespace TicketBox.Application.Features.CQRS.Events.Queries
{
    public class GetEventDetailQuery : IRequest<GetEventDetailQueryResult>
    {
        public int EventId { get; set; }
    }
}
