using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Coupons.Results;

namespace TicketBox.Application.Features.CQRS.Coupons.Queries
{
    public class GetCouponQuery : IRequest<List<GetCouponQueryResult>>
    {
    }
}
