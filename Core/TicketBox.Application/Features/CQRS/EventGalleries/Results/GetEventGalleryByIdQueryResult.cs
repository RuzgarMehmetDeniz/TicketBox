namespace TicketBox.Application.Features.CQRS.EventGalleries.Results
{
    public class GetEventGalleryByIdQueryResult
    {
        public int EventGalleryId { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string ImageUrl { get; set; }
    }
}