namespace TicketBox.WebUI.Models
{
    public class CategoryCardViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? IconUrl { get; set; }
        public int EventCount { get; set; }
    }
}
