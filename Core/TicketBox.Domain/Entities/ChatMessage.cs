using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class ChatMessage
    {
        public int ChatMessageId { get; set; }

        public int ChatSessionId { get; set; }
        public ChatSession ChatSession { get; set; }

        public string Sender { get; set; }     // "User" veya "Bot"
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
    }
}
