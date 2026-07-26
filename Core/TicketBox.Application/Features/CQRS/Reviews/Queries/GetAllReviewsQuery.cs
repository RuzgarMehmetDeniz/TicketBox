using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.Reviews.Results;

namespace TicketBox.Application.Features.CQRS.Reviews.Queries
{
    public class GetAllReviewsQuery : IRequest<List<GetReviewQueryResult>>
    {
    }
}