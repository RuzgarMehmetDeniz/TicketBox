using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; }              // "YAZ2026" gibi
        public decimal DiscountPercentage { get; set; } // %10, %20 vb.
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public int? UsageLimit { get; set; }            // Kaç kez kullanılabilir (null = sınırsız)
        public int UsedCount { get; set; }

        public ICollection<Ticket> Tickets { get; set; }   // Bu kuponu kullanan biletler

    }
}
