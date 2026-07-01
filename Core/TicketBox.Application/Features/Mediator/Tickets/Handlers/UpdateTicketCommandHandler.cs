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
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
    {
        private readonly TicketContext _context;

        public UpdateTicketCommandHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var value = await _context.Tickets.FindAsync(request.TicketId);
            if (value == null) return;

            value.TicketId = request.TicketId;
            value.EventId = request.EventId;
            value.Price = request.Price;
            value.PurchaseDate = request.PurchaseDate;
            value.AttendeeId = request.AttendeeId;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
