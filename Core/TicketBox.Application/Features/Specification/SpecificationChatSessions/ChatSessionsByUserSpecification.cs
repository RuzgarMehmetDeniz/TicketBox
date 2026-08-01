using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationChatSessions
{
    public class ChatSessionsByUserSpecification : BaseSpecification<ChatSession>
    {
        public ChatSessionsByUserSpecification(string appUserId) : base(cs => cs.AppUserId == appUserId)
        {
            AddInclude(cs => cs.Messages);
            ApplyOrderByDescending(cs => cs.StartedDate);
        }
    }
}