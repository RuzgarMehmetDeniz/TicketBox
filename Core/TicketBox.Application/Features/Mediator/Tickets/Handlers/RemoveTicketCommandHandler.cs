using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Tickets.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class RemoveTicketCommandHandler : IRequestHandler<RemoveTicketCommand>
    {
        private readonly TicketContext _context;

        public RemoveTicketCommandHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveTicketCommand request, CancellationToken cancellationToken)
        {
            var value = await _context.Tickets.FindAsync(request.TicketId);
            _context.Tickets.Remove(value);
             await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
