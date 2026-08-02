using System;

namespace TicketBox.WebUI.Models
{
    public class HeroViewModel
    {
        public int ActiveEventsCount { get; set; }
        public int UsersCount { get; set; }
        public int CitiesCount { get; set; }
        public double AverageRating { get; set; }
        public int TicketsThisWeek { get; set; }

        public FeaturedEventVm? Featured { get; set; }
        public FeaturedEventVm? LowStock { get; set; }
    }

    public class FeaturedEventVm
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
        public decimal Price { get; set; }
        public int RemainingCapacity { get; set; }
        public string CategoryName { get; set; }
    }
}