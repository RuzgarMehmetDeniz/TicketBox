using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Tickets.Results
{
    public class GetByIdTicketQueryResult
    {
        public int TicketId { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; }

        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}
