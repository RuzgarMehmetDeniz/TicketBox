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
    public class GetByIdEventQueryHandler : IRequestHandler<GetByIdEventQuery, GetByIdEventQueryResult>
    {
        private readonly TicketContext _context;

        public GetByIdEventQueryHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task<GetByIdEventQueryResult> Handle(GetByIdEventQuery request, CancellationToken cancellationToken)
        {
            var value = await _context.Events
                .Where(x => x.EventId == request.Id)
                .Select(x => new GetByIdEventQueryResult
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
                }).FirstOrDefaultAsync(cancellationToken);
            return value;
        }
    }
}
