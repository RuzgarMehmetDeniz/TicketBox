using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationChatMessages
{
    public class ChatMessagesBySessionSpecification : BaseSpecification<ChatMessage>
    {
        public ChatMessagesBySessionSpecification(int chatSessionId) : base(m => m.ChatSessionId == chatSessionId)
        {
            ApplyOrderBy(m => m.SentDate);
        }
    }
}