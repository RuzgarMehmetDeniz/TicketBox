using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Tickets.Queries;
using TicketBox.Application.Features.Mediator.Tickets.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, List<GetTicketQueryResult>>
    {
        private readonly TicketContext _context;

        public GetTicketQueryHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var value =await _context.Tickets.Select(x => new GetTicketQueryResult
            {
                TicketId = x.TicketId,
                EventId = x.EventId,
                AttendeeId = x.AttendeeId,
                PurchaseDate = x.PurchaseDate,
                Price = x.Price
            }).ToListAsync(cancellationToken);
            return value;
        }
    }
}
