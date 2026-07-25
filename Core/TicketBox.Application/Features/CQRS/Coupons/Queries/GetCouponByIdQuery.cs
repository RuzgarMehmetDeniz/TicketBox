using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Coupons.Results;

namespace TicketBox.Application.Features.CQRS.Coupons.Queries
{
    public class GetCouponByIdQuery : IRequest<GetCouponByIdQueryResult>
    {
        public GetCouponByIdQuery(int id)
        {
            CouponId = id;
        }

        public int CouponId { get; set; }
    }
}
