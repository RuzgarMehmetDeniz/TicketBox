using System;

namespace TicketBox.Application.Features.CQRS.ChatMessages.Results
{
    public class GetChatMessageByIdQueryResult
    {
        public int ChatMessageId { get; set; }
        public int ChatSessionId { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
    }
}