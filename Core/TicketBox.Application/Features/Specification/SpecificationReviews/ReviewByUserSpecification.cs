using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationReviews
{
    public class ReviewByUserSpecification : BaseSpecification<Review>
    {
        public ReviewByUserSpecification(string appUserId) : base(r => r.AppUserId == appUserId)
        {
            AddInclude(r => r.Event);
            ApplyOrderByDescending(r => r.CreatedDate);
        }
    }
}