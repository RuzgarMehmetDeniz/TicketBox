using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Favorite
    {
        public int FavoriteId { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
