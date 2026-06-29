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
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly TicketContext _context;

        public UpdateEventCommandHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var values = await _context.Events.FindAsync(request.EventId);

            if (values == null)
                return;

            values.Title = request.Title;
            values.Description = request.Description;
            values.EventDate = request.EventDate;
            values.Location = request.Location;
            values.Capacity = request.Capacity;
            values.Price = request.Price;
            values.ImageUrl = request.ImageUrl;
            values.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}