using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class EventGallery
    {
        public int EventGalleryId { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public string ImageUrl { get; set; }
    }
}
