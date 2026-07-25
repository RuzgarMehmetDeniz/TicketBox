using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int Rating { get; set; }        // 1-5 arası
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
