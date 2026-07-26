using MediatR;
using System;

namespace TicketBox.Application.Features.CQRS.Events.Commands
{
    public class CreateEventCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string CreatedByUserId { get; set; }
        public int CategoryId { get; set; }
    }
}