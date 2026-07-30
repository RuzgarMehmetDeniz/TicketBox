namespace TicketBox.WebUI.Models
{
    public class EventCardViewModel
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
        public decimal Price { get; set; }
        public string OrganizerName { get; set; }
        public int AttendeeCount { get; set; }
        public int FillPercentage { get; set; }
        public double AverageRating { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsLive { get; set; }
    }
}
