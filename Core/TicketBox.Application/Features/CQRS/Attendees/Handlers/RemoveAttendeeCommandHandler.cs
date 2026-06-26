using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Attendees.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class RemoveAttendeeCommandHandler
    {
        private readonly TicketContext _context;
        public RemoveAttendeeCommandHandler(TicketContext context)
        {
            _context = context;
        }
        public async Task Handle(RemoveAttendeeCommand command)
        {
            var value = await _context.Attendees.FindAsync(command.AttendeeId);
            _context.Attendees.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
