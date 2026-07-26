using System;

namespace TicketBox.Application.Features.CQRS.Reviews.Results
{
    public class GetReviewQueryResult
    {
        public int ReviewId { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string AppUserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}