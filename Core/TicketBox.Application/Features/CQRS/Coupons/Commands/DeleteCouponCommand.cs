using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.Coupons.Commands
{
    public class DeleteCouponCommand : IRequest
    {
        public DeleteCouponCommand(int id)
        {
            CouponId = id;
        }
        public int CouponId { get; set; }
    }
}
