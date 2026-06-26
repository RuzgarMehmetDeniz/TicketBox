using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Attendees.Queries;
using TicketBox.Application.Features.CQRS.Attendees.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class GetAttendeeByIdQueryHandler
    {
        private readonly TicketContext _context;
        public GetAttendeeByIdQueryHandler(TicketContext context)
        {
            _context = context;
        }
        public async Task<GetAttendeeByIdQueryResult> Handle(GetAttendeeByIdQuery query)
        {
           var value = await _context.Attendees.Where(x => x.AttendeeId == query.AttendeeId).Select(x => new GetAttendeeByIdQueryResult
            {
                AttendeeId = x.AttendeeId,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email
            }).FirstOrDefaultAsync();
            return value;
        }
    }
}
