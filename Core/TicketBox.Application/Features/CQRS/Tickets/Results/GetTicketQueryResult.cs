using System;

namespace TicketBox.Application.Features.CQRS.Tickets.Results
{
    public class GetTicketQueryResult
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string AppUserId { get; set; }
        public string CustomerFullName { get; set; }
        public string PNRCode { get; set; }
        public string? TicketImageUrl { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public bool IsEmailSent { get; set; }
        public int? CouponId { get; set; }
    }
}