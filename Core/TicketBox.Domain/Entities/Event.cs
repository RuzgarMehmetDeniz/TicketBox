using System;
using System.Collections.Generic;

namespace TicketBox.Domain.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public int RemainingCapacity { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public string CreatedByUserId { get; set; }
        public AppUser CreatedByUser { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<EventGallery> Galleries { get; set; }
    }
}