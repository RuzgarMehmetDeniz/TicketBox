using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Attendees.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class CreateAttendeeCommandHandler
    {
        private readonly TicketContext _context;
        public CreateAttendeeCommandHandler(TicketContext context)
        {
            _context = context;
        }
        public async Task Handle(CreateAttendeeCommand command)
        {
            var attendee = new Attendee
            {
                Name = command.Name,
                Surname = command.Surname,
                Email = command.Email
            };
            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();
        }
    }
}
