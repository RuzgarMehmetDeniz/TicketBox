using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.Events.Results
{
    public class GetEventDetailQueryResult
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
        public string CategoryName { get; set; }
        public string OrganizerName { get; set; }
        public double AverageRating { get; set; }
        public int FillPercentage { get; set; }
        public List<string> GalleryImageUrls { get; set; } = new();
        public List<EventDetailReviewResult> Reviews { get; set; } = new();
    }

    public class EventDetailReviewResult
    {
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
