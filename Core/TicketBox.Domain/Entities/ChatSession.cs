using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class ChatSession
    {
        public int ChatSessionId { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public DateTime StartedDate { get; set; }
        public ICollection<ChatMessage> Messages { get; set; }
    }
}
