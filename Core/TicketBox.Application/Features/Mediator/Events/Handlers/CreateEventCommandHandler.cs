using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
    {
        private readonly TicketContext _context;

        public CreateEventCommandHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var values = new Event
            {
                Title = request.Title,
                Description = request.Description,
                EventDate = request.EventDate,
                Location = request.Location,
                Capacity = request.Capacity,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                CategoryId = request.CategoryId
            };

            await _context.Events.AddAsync(values);
            await _context.SaveChangesAsync();
        }
    }
}