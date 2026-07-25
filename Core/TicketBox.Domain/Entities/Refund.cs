using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Refund
    {
        public int RefundId { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        public decimal RefundAmount { get; set; }
        public string Reason { get; set; }            // İptal sebebi
        public string Status { get; set; }             // "Beklemede", "Onaylandı", "Reddedildi"
        public DateTime RequestDate { get; set; }
        public DateTime? ProcessedDate { get; set; }   // İşlemin tamamlandığı tarih
    }
}
