using System;

namespace TicketBox.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }   // "Kredi Kartı", "Havale" vb.
        public string Status { get; set; }           // "Başarılı", "Başarısız", "Beklemede"
        public DateTime PaymentDate { get; set; }
        public string? TransactionReference { get; set; } // Ödeme sağlayıcıdan dönen referans no
    }
}