using System;
using System.Collections.Generic;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Results
{
    public class ChatSessionWithMessagesResult
    {
        public int ChatSessionId { get; set; }
        public string AppUserId { get; set; }
        public DateTime StartedDate { get; set; }
        public List<ChatMessageItem> Messages { get; set; } = new();
    }

    public class ChatMessageItem
    {
        public int ChatMessageId { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
    }
}