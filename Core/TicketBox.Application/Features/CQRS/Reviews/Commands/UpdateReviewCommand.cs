using MediatR;

namespace TicketBox.Application.Features.CQRS.Reviews.Commands
{
    public class UpdateReviewCommand : IRequest<bool>
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}