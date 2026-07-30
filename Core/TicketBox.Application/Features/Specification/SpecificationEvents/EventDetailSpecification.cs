using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationEvents
{
    public class EventDetailSpecification : BaseSpecification<Event>
    {
        public EventDetailSpecification(int eventId) : base(e => e.EventId == eventId)
        {
            AddInclude(e => e.Category);
            AddInclude(e => e.CreatedByUser);
            AddInclude(e => e.Galleries);
            AddInclude(e => e.Reviews);
            AddInclude("Reviews.AppUser"); // ThenInclude yerine string path
        }
    }
}
