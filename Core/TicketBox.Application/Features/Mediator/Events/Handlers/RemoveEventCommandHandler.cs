using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class RemoveEventCommandHandler : IRequestHandler<RemoveEventCommand>
    {
        private readonly TicketContext _context;

        public RemoveEventCommandHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveEventCommand request, CancellationToken cancellationToken)
        {
            var values = await _context.Events.FindAsync(request.EventId);
            _context.Events.Remove(values);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}