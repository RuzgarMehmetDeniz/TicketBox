using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.Reviews.Results;

namespace TicketBox.Application.Features.CQRS.Reviews.Queries
{
    public class GetMyReviewsQuery : IRequest<List<GetReviewQueryResult>>
    {
        public string AppUserId { get; set; }
    }
}