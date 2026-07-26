using TicketBox.Application.Features.Specification;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Specification.SpecificationReviews
{
    public class ReviewWithEventSpecification : BaseSpecification<Review>
    {
        public ReviewWithEventSpecification()
        {
            AddInclude(r => r.Event);
        }

        public ReviewWithEventSpecification(int reviewId) : base()
        {
            Criteria = r => r.ReviewId == reviewId;
            AddInclude(r => r.Event);
        }
    }
}