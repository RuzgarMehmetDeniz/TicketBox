using MediatR;

namespace TicketBox.Application.Features.CQRS.Reviews.Commands
{
    public class DeleteReviewCommand : IRequest<bool>
    {
        public int ReviewId { get; set; }
    }
}