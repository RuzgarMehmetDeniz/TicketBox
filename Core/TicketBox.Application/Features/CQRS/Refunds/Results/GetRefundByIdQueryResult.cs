using System;

namespace TicketBox.Application.Features.CQRS.Refunds.Results
{
    public class GetRefundByIdQueryResult
    {
        public int RefundId { get; set; }
        public int TicketId { get; set; }
        public string PNRCode { get; set; }
        public int PaymentId { get; set; }
        public decimal RefundAmount { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
    }
}