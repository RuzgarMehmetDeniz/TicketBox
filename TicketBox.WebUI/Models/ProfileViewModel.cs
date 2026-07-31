using System;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.Favorites.Results;
using TicketBox.Application.Features.CQRS.Reviews.Results;
using TicketBox.Application.Features.CQRS.Tickets.Results;

namespace TicketBox.WebUI.Models
{
    public class ProfileViewModel
    {
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<GetTicketQueryResult> Tickets { get; set; } = new();
        public List<GetFavoriteQueryResult> Favorites { get; set; } = new();
        public List<GetReviewQueryResult> Reviews { get; set; } = new();
    }
}