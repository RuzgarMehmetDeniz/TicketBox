using MediatR;
using System.Collections.Generic;
using TicketBox.Application.Features.CQRS.Payments.Results;

namespace TicketBox.Application.Features.CQRS.Payments.Queries
{
    public class GetAllPaymentsQuery : IRequest<List<GetPaymentQueryResult>>
    {
    }
}