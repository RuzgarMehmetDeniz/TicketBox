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
    public class GetByIdTicketQueryHandler : IRequestHandler<GetByIdTicketQuery, GetByIdTicketQueryResult>
    {
        private readonly TicketContext _context;

        public GetByIdTicketQueryHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task<GetByIdTicketQueryResult> Handle(GetByIdTicketQuery request, CancellationToken cancellationToken)
        {
            var value = await _context.Tickets.Where( x => x.TicketId == request.Id).Select(x => new GetByIdTicketQueryResult
            {
                TicketId = x.TicketId,
                EventId = x.EventId,
                AttendeeId = x.AttendeeId,
                PurchaseDate = x.PurchaseDate,
                Price = x.Price
            }).FirstOrDefaultAsync(cancellationToken);
            return value;
        }
    }
}
