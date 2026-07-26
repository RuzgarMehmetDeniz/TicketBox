using System;

namespace TicketBox.Application.Features.CQRS.Payments.Results
{
    public class GetPaymentQueryResult
    {
        public int PaymentId { get; set; }
        public int TicketId { get; set; }
        public string PNRCode { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? TransactionReference { get; set; }
    }
}