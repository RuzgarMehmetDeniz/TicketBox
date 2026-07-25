using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.Coupons.Results
{
    public class GetCouponByIdQueryResult
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public int? UsageLimit { get; set; }
        public int UsedCount { get; set; }
    }
}
