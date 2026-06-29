using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Events.Queries;
using TicketBox.Application.Features.Mediator.Events.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, List<GetEventQuertResult>>
    {
        private readonly TicketContext _context;

        public GetEventQueryHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task<List<GetEventQuertResult>> Handle(
            GetEventQuery request,
            CancellationToken cancellationToken)
        {
            var values = await _context.Events
                .Select(x => new GetEventQuertResult
                {
                    EventId = x.EventId,
                    Title = x.Title,
                    Description = x.Description,
                    EventDate = x.EventDate,
                    Location = x.Location,
                    Capacity = x.Capacity,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.CategoryId
                })
                .ToListAsync(cancellationToken);

            return values;
        }
    }
}
