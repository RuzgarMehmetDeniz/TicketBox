using MediatR;

namespace TicketBox.Application.Features.CQRS.Reviews.Commands
{
    public class CreateReviewCommand : IRequest<int>
    {
        public int EventId { get; set; }
        public string AppUserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}