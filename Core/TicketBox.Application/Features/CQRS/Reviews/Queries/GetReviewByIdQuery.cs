using MediatR;
using TicketBox.Application.Features.CQRS.Reviews.Results;

namespace TicketBox.Application.Features.CQRS.Reviews.Queries
{
    public class GetReviewByIdQuery : IRequest<GetReviewByIdQueryResult>
    {
        public int ReviewId { get; set; }
    }
}