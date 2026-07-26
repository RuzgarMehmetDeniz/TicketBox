using System;

namespace TicketBox.Application.Features.CQRS.ChatSessions.Results
{
    public class GetChatSessionByIdQueryResult
    {
        public int ChatSessionId { get; set; }
        public string AppUserId { get; set; }
        public DateTime StartedDate { get; set; }
    }
}