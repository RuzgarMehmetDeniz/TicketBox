using System;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Results
{
    public class GetChatSessionQueryResult
    {
        public int ChatSessionId { get; set; }
        public string AppUserId { get; set; }
        public DateTime StartedDate { get; set; }
    }
}