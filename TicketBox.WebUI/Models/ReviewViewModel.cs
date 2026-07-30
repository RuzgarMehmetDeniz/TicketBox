namespace TicketBox.WebUI.Models
{
    public class ReviewViewModel
    {
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
