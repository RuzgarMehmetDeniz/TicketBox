using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.Refunds.Results;

namespace TicketBox.Application.Features.CQRS.Refunds.Queries
{
    public class GetAllRefundsQuery : IRequest<List<GetRefundQueryResult>>
    {
    }
}