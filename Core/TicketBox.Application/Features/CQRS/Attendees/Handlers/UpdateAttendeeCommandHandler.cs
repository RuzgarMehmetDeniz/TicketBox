using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Attendees.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class UpdateAttendeeCommandHandler
    {
        private readonly TicketContext _context;

        public UpdateAttendeeCommandHandler(TicketContext context)
        {
            _context = context;
        }
        public async Task Handler(UpdateAttendeeCommand command)
        {
            var value = await _context.Attendees.FindAsync(command.AttendeeId);
            value.Name = command.Name;
            value.Surname = command.Surname;
            value.Email = command.Email;
            await _context.SaveChangesAsync();

        }
    }
}
