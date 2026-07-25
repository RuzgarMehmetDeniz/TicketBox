using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TicketBox.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? PreferredCategories { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Event> CreatedEvents { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<ChatSession> ChatSessions { get; set; }
        public ICollection<AuditLog> AuditLogs { get; set; }
    }
}