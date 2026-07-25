using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string Action { get; set; }       // "Bilet Satın Alındı", "Etkinlik İptal Edildi" vb.
        public string? Details { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
