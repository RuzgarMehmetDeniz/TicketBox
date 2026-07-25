using System;

namespace TicketBox.Domain.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string PNRCode { get; set; }
        public string? TicketImageUrl { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public bool IsEmailSent { get; set; }

        public int? CouponId { get; set; }             // Nullable - kupon kullanılmayabilir
        public Coupon? Coupon { get; set; }

        public Payment Payment { get; set; }
        public Refund? Refund { get; set; }
    }
}